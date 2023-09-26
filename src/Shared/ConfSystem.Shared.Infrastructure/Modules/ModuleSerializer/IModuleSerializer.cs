namespace ConfSystem.Shared.Infrastructure.Modules;

public interface IModuleSerializer
{
    byte[] Serializer<T>(T value);
    T Deserialize<T>(byte[] value);
    object Deserialize(byte[] value, Type type);
}