using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.Repositories
{
    public class FilesRepository : BaseRepository, IFilesRepository
    {
        public FilesRepository(FilesStorageContext context, Action<Exception> commandFailure) 
            : base(context, commandFailure)
        {
        }

        public void Add(StorageFile entity, int storageId)
        {            
            Command(c =>
            {
                var storage = Query<Storage>(false).FirstOrDefault(s => s.Id == storageId);
                entity.Storage = storage;
                c.Entry(entity).State = EntityState.Added;
            });
        }

        public void Delete(int id)
        {
            DeleteEntity<StorageFile, int>(id);
        }

        public void Update(StorageFile entity)
        {
            CustomEntityUpdate(entity, entity.Id, f => f.Name, f => f.Description, f => f.FileType);
        }

        public StorageFile GetById(int id)
        {
            return FindEntityById<StorageFile, int>(id);
        }

        public IEnumerable<StorageFile> GetAllFilesFromStorage(int storageId)
        {
            return FindByWithEagerLoad<StorageFile>(f => f.Storage.Id == storageId);
        }
    }
}
