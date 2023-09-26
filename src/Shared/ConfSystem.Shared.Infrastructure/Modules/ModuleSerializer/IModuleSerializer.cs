namespace ConfSystem.Shared.Infrastructure.Modules.ModuleSerializer;

public interface IModuleSerializer
{
    byte[] Serializer<T>(T value);
    T Deserialize<T>(byte[] value);
    object Deserialize(byte[] value, Type type);
}