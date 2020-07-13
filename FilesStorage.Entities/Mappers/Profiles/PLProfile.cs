using System;

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
            //CreateMap<FileView, FileDto>()
            //    .IncludeMembers(f => f.Id);

            CreateMap<FileDto, CreateEditFileView>();
            CreateMap<CreateEditFileView, FileDto>();

            CreateMap<AddFileView, FileDto>();


            //Login

            CreateMap<UserLoginView, UserSignInDto>();
            CreateMap<UserSignInDto, UserLoginView>();

            //Users

            

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
