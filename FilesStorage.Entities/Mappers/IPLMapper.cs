namespace FilesStorage.Entities.Mappers
{
    public interface IPLMapper
    {
        TDestination Map<TDestination, TSource>(TSource source);
    }
}
