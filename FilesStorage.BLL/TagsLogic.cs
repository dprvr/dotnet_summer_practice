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
        private readonly IConventions _conventions;
        private readonly IBLMapper _mapper;

        public TagsLogic(ITagsRepository tagsRepository, IConventions conventions,
            IBLMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _conventions = conventions;
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
            var found = _tagsRepository.GetAll(_conventions.GetStorageIdByUserLogin(UserLogin));
            return _mapper.Map<IEnumerable<TagDto>, IEnumerable<StorageTag>>(found);
        }

        public void AddTag(TagDto tag, string login)
        {
            var entity = _mapper.Map<StorageTag, TagDto>(tag);
            _tagsRepository.Add(entity, _conventions.GetStorageIdByUserLogin(login));
        }
    }
}
