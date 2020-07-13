namespace FilesStorage.Entities.Mappers
{
    public interface IBLMapper
    {
        TDestination Map<TDestination, TSource>(TSource source);
    }
}
