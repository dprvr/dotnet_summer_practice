using FilesStorage.Entities.Mappers.Profiles;

namespace FilesStorage.Entities.Mappers
{
    public class MappersFactory
    {
        public static IPLMapper CreateDtosAndViewModelsMapper()
        {
            return new AMapper(new PLProfile());
        }

        public static IBLMapper CreateDtoAndEntityModelsMapper()
        {
            return new AMapper(new BLProfile());
        }
    }
}
