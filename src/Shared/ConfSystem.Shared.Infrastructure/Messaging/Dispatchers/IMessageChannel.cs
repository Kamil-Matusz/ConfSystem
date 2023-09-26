using System.Threading.Channels;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Shared.Infrastructure.Messaging.Dispatchers;

public interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get;  }
    ChannelWriter<IMessage> Writer { get;  }
}