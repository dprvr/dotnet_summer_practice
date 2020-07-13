
using AutoMapper;

using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;

namespace FilesStorage.Entities.Mappers.Profiles
{
    public class BLProfile : Profile
    {
        public BLProfile()
        {
            //Files
            CreateMap<FileDto, StorageFile>()
                .ForMember(f => f.Tags, opt => opt.Ignore());
            CreateMap<StorageFile, FileDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(f => f.Id));

            //Tags
            CreateMap<StorageTag, TagDto>();
            CreateMap<TagDto, StorageTag>();

            //Users
            CreateMap<User, UserSignUpDto>();
            CreateMap<UserSignUpDto, User>()
                .ForMember(s => s.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(s => s.LastName));

            CreateMap<UserSignUpDto, Account>()
                .ForMember(a => a.HashedPassword, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Login, opt => opt.Ignore());

            CreateMap<Account, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(d => d.Birthday, opt => opt.Ignore())
                .ForMember(d => d.Email, opt => opt.Ignore())
                .ForMember(d => d.FirstName, opt => opt.Ignore())
                .ForMember(d => d.Gender, opt => opt.Ignore())
                .ForMember(d => d.LastName, opt => opt.Ignore());            

            
        }
    }
}
