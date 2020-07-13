using System.Collections.Generic;

using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL.Interfaces
{
    public interface ITagsLogic
    {
        void AddTag(TagDto tag, string login);        
        void EditTag(TagDto tag);
        void DeleteTagById(int id);

        TagDto FindTagById(int tagId);

        IEnumerable<TagDto> GetAllUserTags(string UserLogin);
    }
}
