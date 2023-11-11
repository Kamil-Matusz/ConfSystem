using Chronicle;
using ConfSystem.Modules.Saga.Messages;
using ConfSystem.Shared.Abstractions.Messaging;
using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Saga.InviteSpeaker;

internal class InviteSpeakerSaga : Saga<InviteSpeakerSaga.SagaData>,
    ISagaStartAction<SignedUp>,
    ISagaAction<SpeakerCreated>,
    ISagaAction<SignedIn>
{
    
    internal class SagaData
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool SpeakerCreated { get; set; }

        public readonly Dictionary<string, string> InvitedSpeakers = new()
        {
            ["testspeaker1@confsystem.io"] = "Kamil Matusz",
            ["testspeaker2@confsystem.io"] = "Adrian Myszak",
        };
    }

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            SignedUp k => k.UserId.ToString(),
            SignedIn k => k.UserId.ToString(),
            SpeakerCreated k => k.Id.ToString(),
            _ => base.ResolveId(message, context)
        };

    private readonly IModuleClient _moduleClient;
    private readonly IMessageBroker _messageBroker;
    
    public InviteSpeakerSaga(IModuleClient moduleClient, IMessageBroker messageBroker)
    {
        _moduleClient = moduleClient;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(SignedUp message, ISagaContext context)
    {
        var (userId, email) = message;
        if (Data.InvitedSpeakers.TryGetValue(email, out var fullName))
        {
            Data.Email = email;
            Data.FullName = fullName;

            await _moduleClient.SendAsync("speakers/create", new
            {
                Id = userId, Email = email, FullName = fullName, Bio = "Lorem Ipsum"
            });
                
            return;
        }

        await CompleteAsync();
    }

    public Task CompensateAsync(SignedUp message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(SpeakerCreated message, ISagaContext context)
    {
        Data.SpeakerCreated = true;
        return Task.CompletedTask;
    }

    public Task CompensateAsync(SpeakerCreated message, ISagaContext context)
        => Task.CompletedTask;

    public async Task HandleAsync(SignedIn message, ISagaContext context)
    {
        if (Data.SpeakerCreated)
        {
            await _messageBroker.PublishAsync(new SendWelcomeMessage(Data.Email, Data.FullName));
            await CompleteAsync();
        }
    }

    public Task CompensateAsync(SignedIn message, ISagaContext context)
        => Task.CompletedTask;
}