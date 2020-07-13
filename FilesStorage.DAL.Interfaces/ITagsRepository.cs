using System.Collections.Generic;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface ITagsRepository
    {
        void Add(StorageTag entity, int storageId);
        void Delete(int id);
        void Update(StorageTag entity);

        StorageTag FindById(int id);
        IEnumerable<StorageTag> GetAll(int storageId);
    }
}
