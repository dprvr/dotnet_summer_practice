using System.Collections.Generic;
using FilesStorage.BLL.Interfaces;
using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;
using FilesStorage.Entities.Mappers;

namespace FilesStorage.BLL
{
    public class TagsLogic : ITagsLogic
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IBLMapper _mapper;

        public TagsLogic(ITagsRepository tagsRepository, IAccountsRepository accountsRepository,
            IBLMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _accountsRepository = accountsRepository;
            _mapper = mapper;
        }

        public void EditTag(TagDto tag)
        {
            _tagsRepository.Update(_mapper.Map<StorageTag, TagDto>(tag));
        }

        public void DeleteTagById(int id)
        {
            _tagsRepository.Delete(id);
        }

        public TagDto FindTagById(int tagId)
        {
            var found = _tagsRepository.FindById(tagId);
            return _mapper.Map<TagDto, StorageTag>(found);
        }

        public IEnumerable<TagDto> GetAllUserTags(string UserLogin)
        {
            var found = _tagsRepository.GetAll(_accountsRepository.FindByLogin(UserLogin).StorageId);
            return _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(found);
        }

        public void AddTag(TagDto tag, string login)
        {
            var account = _accountsRepository.FindByLogin(login);
            var entity = _mapper.Map<StorageTag, TagDto>(tag);
            _tagsRepository.Add(entity, account.StorageId);
        }
    }
}
