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
        private readonly IAccountsRepository _accountsRepository;
        private readonly IFilesAndTagsRepository _filesAndTagsRepo;
        private readonly ISearchQueryParser _parser;
        private readonly IBLMapper _mapper;

        public FilesLogic(IFilesRepository filesRepository, IAccountsRepository accountsRepository,
            IBLMapper mapper, IFilesAndTagsRepository filesAndTagsRepo, ISearchQueryParser parser)
        {
            _filesRepository = filesRepository;
            _accountsRepository = accountsRepository;
            _mapper = mapper;
            _filesAndTagsRepo = filesAndTagsRepo;
            _parser = parser;
        }

        public FileDto FindFileById(int fileId)
        {
            var founded = _filesRepository.GetById(fileId);
            var tags = _filesAndTagsRepo.GetFileTags(fileId);
            var tagsDtos = _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(tags);
            var dto = _mapper.Map<FileDto, StorageFile>(founded);
            dto.Tags = tagsDtos.ToList();
            return dto;
        }

        public void EditFile(FileDto fileDto)
        {
            _filesRepository.Update(_mapper.Map<StorageFile, FileDto>(fileDto));
        }

        public void DeleteFile(int fileId)
        {
            _filesRepository.Delete(fileId);
        }

        public IEnumerable<FileDto> SearchUserFiles(FilesSearchDto searchDto)
        {
            var searchOpt = _parser.ParseQuery(searchDto);
            searchOpt.StorageId = _accountsRepository.FindByLogin(searchDto.LoginName).StorageId;
            var found = _filesAndTagsRepo.Search(searchOpt);
            var mapped = _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(found);
            return mapped;
        }

        public IEnumerable<FileDto> GetAllUserFiles(string userLogin)
        {
            var account = _accountsRepository.FindByLogin(userLogin);
            var files = _filesRepository.GetAllFilesFromStorage(account.StorageId);
            return _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(files);
        }

        public void Add(FileDto fileDto, string login)
        {
            var account = _accountsRepository.FindByLogin(login);
            var entity = _mapper.Map<StorageFile, FileDto>(fileDto);
            _filesRepository.Add(entity, account.StorageId);
        }

        public IEnumerable<FileDto> GetAllUserFilesWithTags(string userLogin)
        {
            var account = _accountsRepository.FindByLogin(userLogin);
            var files = _filesRepository.GetAllFilesFromStorage(account.StorageId);
            var dtos = _mapper.Map<IEnumerable<FileDto>, IEnumerable<StorageFile>>(files);
            var dtosWithTags = dtos.Select(f =>
            {
                var tags = _filesAndTagsRepo.GetFileTags(f.Id);
                var tagDtos = _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(tags);
                f.Tags = tagDtos.ToList();
                return f;
            });
            return dtosWithTags;
        }
    }
}
