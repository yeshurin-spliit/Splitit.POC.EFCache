namespace EFCache.POC.IoC.Generics
{
    public interface IFactory<T>
    {
        T Create();
    }
}