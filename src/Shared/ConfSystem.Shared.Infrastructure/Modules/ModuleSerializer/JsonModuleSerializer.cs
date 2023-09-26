namespace ConfSystem.Shared.Infrastructure.Modules;

internal class JsonModuleSerializer : IModuleSerializer
{
    public byte[] Serializer<T>(T value)
    {
        throw new NotImplementedException();
    }

    public T Deserialize<T>(byte[] value)
    {
        throw new NotImplementedException();
    }

    public object Deserialize(byte[] value, Type type)
    {
        throw new NotImplementedException();
    }
}