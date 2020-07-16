using System.Collections.Generic;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface IFilesRepository
    {
        StorageFile Add(StorageFile entity, int storageId);
        void Delete(int id);
        void Update(StorageFile entity);

        StorageFile GetById(int id);

        IEnumerable<StorageFile> GetAllFilesFromStorage(int storageId);             
    }
}
