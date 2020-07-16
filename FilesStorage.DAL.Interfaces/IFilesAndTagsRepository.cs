using System.Collections.Generic;

using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface IFilesAndTagsRepository
    {
        IEnumerable<StorageTag> GetFileTags(int fileId);
        IEnumerable<StorageFile> GetTagFiles(int tagId);

        void AddTagsToFile(int fileId, params int[] tagsIds);
        void DeleteTagsFromFile(int fileId, params int[] tagsIds);
        void InsertOrUpdateFileTags(int fileId, params int[] newFileTagsIds);

        IEnumerable<StorageFile> Search(SearchOptionsDto searchDto);
    }
}
