using System.Collections.Generic;

using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL.Interfaces
{
    public interface IFilesLogic
    {        
        void Add(FileDto fileDto, string login);
        void EditFile(FileDto fileDto);
        void DeleteFile(int fileId);

        FileDto FindFileById(int fileId);

        IEnumerable<FileDto> GetAllUserFiles(string userLogin);
        IEnumerable<FileDto> GetAllUserFilesWithTags(string userLogin);
        IEnumerable<FileDto> SearchUserFiles(FilesSearchDto searchDto);
    }
}
