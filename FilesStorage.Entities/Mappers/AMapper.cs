using AutoMapper;

namespace FilesStorage.Entities.Mappers
{
    public class AMapper : IBLMapper, IPLMapper
    {
        private readonly Mapper _mapper;

        public AMapper(Profile profile)
        {
            var config = new MapperConfiguration(cnfgrtn => cnfgrtn.AddProfile(profile));
            _mapper = new Mapper(config);
        }

        public TDestination Map<TDestination, TSource>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
