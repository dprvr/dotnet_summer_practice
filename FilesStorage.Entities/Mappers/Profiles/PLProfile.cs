using System;
using System.Linq;

using AutoMapper;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Enums;
using FilesStorage.Entities.ViewModels;

namespace FilesStorage.Entities.Mappers.Profiles
{
    public class PLProfile : Profile
    {
        public PLProfile()
        {
            //Tags
            CreateMap<TagsView, TagDto>();
            CreateMap<TagDto, TagsView>();

            //Files
            CreateMap<FileDto, FileView>()
                .ForMember(f => f.FullName, opt => 
                    opt.MapFrom(d => d.Name + "." + Enum.GetName(typeof(FileType),d.FileType)));

            CreateMap<AddFileView, FileWithTagsDto>();

            CreateMap<FileWithTagsDto, EditFileView>()
                .ForMember(v => v.Tags, opt => opt.MapFrom(d => d.Tags.Select(t => t.Id)));
            CreateMap<EditFileView, FileWithTagsDto>()
                .ForMember(d => d.Tags, opt => opt.MapFrom(v => v.Tags.Select(id => new TagDto { Id = id})));

            CreateMap<FileWithTagsDto, FileFullView>();
            //Login

            CreateMap<UserLoginView, UserSignInDto>();
            CreateMap<UserSignInDto, UserLoginView>();

            CreateMap<CreateUserView, UserSignUpDto>();

            CreateMap<CreateUserView, UserDto>();
            CreateMap<UserDto, CreateUserView>();

            CreateMap<UpdateUserProfileView, UserSignUpDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(v => v.Id));
            CreateMap<UserDto, UpdateUserProfileView>();

            CreateMap<UpdateUserProfileView, UserDto>();
            CreateMap<UserDto, UpdateUserProfileView>();

            CreateMap<UserDto, UserProfileView>()
                .ForMember(p => p.FullName, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"));
                
            CreateMap<UserProfileView, UserDto>();
            
        }
        
    }
}
