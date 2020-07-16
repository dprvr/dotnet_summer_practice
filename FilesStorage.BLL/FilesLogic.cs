using System;
using System.Collections.Generic;
using System.Linq;

using FilesStorage.BLL.Interfaces;
using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;
using FilesStorage.Entities.Mappers;

namespace FilesStorage.BLL
{
    public class FilesLogic : IFilesLogic
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IFilesAndTagsRepository _filesAndTagsRepo;
        private readonly ISearchQueryParser _parser;
        private readonly IBLMapper _mapper;
        private readonly IConventions _conventions;

        public FilesLogic(IFilesRepository filesRepository, IBLMapper mapper, IFilesAndTagsRepository filesAndTagsRepo,
            ISearchQueryParser parser, IConventions conventions)
        {
            _filesRepository = filesRepository;
            _mapper = mapper;
            _filesAndTagsRepo = filesAndTagsRepo;
            _parser = parser;
            _conventions = conventions;
        }

        public FileWithTagsDto FindFileById(int fileId)
        {
            var founded = _filesRepository.GetById(fileId);
            var tags = _filesAndTagsRepo.GetFileTags(fileId);
            var tagsDtos = _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(tags);
            var dto = _mapper.Map<FileWithTagsDto, StorageFile>(founded);
            dto.Tags = tagsDtos.ToList();
            return dto;
        }

        public void EditFile(FileWithTagsDto fileDto)
        {
            _filesRepository.Update(_mapper.Map<StorageFile, FileWithTagsDto>(fileDto));
            if(fileDto.Tags != null && fileDto.Tags.Any())
                _filesAndTagsRepo.InsertOrUpdateFileTags(fileDto.Id, fileDto.Tags.Select(t => t.Id).ToArray());
        }

        public void DeleteFile(int fileId)
        {
            _filesRepository.Delete(fileId);
        }

        public IEnumerable<FileDto> SearchUserFiles(FilesSearchDto searchDto)
        {
            var searchOpt = _parser.ParseQuery(searchDto);
            searchOpt.StorageId =_conventions.GetStorageIdByUserLogin(searchDto.LoginName);
            var found = _filesAndTagsRepo.Search(searchOpt);
            var mapped = _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(found);
            return mapped;
        }

        public IEnumerable<FileDto> GetAllUserFiles(string userLogin)
        {
            var files = _filesRepository.GetAllFilesFromStorage(_conventions.GetStorageIdByUserLogin(userLogin));
            return _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(files);
        }

        public void Add(FileWithTagsDto fileDto, string login)
        {
            var entity = _mapper.Map<StorageFile, FileWithTagsDto>(fileDto);
            var addedFile = _filesRepository.Add(entity,_conventions.GetStorageIdByUserLogin(login));
            if(fileDto.Tags != null && fileDto.Tags.Any())
                _filesAndTagsRepo.InsertOrUpdateFileTags(addedFile.Id, fileDto.Tags.Select(t => t.Id).ToArray());
        }

        public IEnumerable<FileWithTagsDto> GetAllUserFilesWithTags(string userLogin)
        {
            //var files = _filesRepository.GetAllFilesFromStorage(_conventions.GetStorageIdByUserLogin(userLogin));
            //var dtos = _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(files);
            //var dtosWithTags = dtos.Select(f =>
            //{
            //    var tags = _filesAndTagsRepo.GetFileTags(f.Id);
            //    var tagDtos = _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(tags);
            //    f.Tags = tagDtos.ToList();
            //    return f;
            //});
            //return dtosWithTags;
            throw new NotImplementedException();
        }
    }
}
