using System.Collections.Generic;

using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL.Interfaces
{
    public interface IFilesLogic
    {        
        void Add(FileWithTagsDto fileDto, string login);
        void EditFile(FileWithTagsDto fileDto);
        void DeleteFile(int fileId);

        FileWithTagsDto FindFileById(int fileId);

        IEnumerable<FileDto> GetAllUserFiles(string userLogin);
        IEnumerable<FileWithTagsDto> GetAllUserFilesWithTags(string userLogin);
        IEnumerable<FileDto> SearchUserFiles(FilesSearchDto searchDto);
    }
}
