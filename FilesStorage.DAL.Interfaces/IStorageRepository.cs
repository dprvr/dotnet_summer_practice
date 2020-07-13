using System.Collections.Generic;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface IStorageRepository
    {
        Storage Add(Storage entity);
        void Delete(int id);
        void Update(Storage entity);

        Storage FindById(int id);
        IEnumerable<Storage> GetAll();
    }
}
