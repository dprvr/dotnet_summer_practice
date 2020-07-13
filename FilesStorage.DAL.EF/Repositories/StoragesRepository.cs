using System;
using System.Collections.Generic;
using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.Repositories
{
    public class StoragesRepository : BaseRepository, IStorageRepository
    {
        public StoragesRepository(FilesStorageContext context, Action<Exception> commandFailure) : base(context, commandFailure)
        {
        }

        public Storage Add(Storage entity)
        {
            Storage storage = null;
            Command(c =>
            {
                storage = c.Set<Storage>().Add(entity);
            });
            return storage;
        }

        public void Delete(int id)
        {
            DeleteEntity<Storage, int>(id);
        }

        public void Update(Storage entity)
        {
            UpdateEntity<Storage>(entity);
        }

        public Storage FindById(int id)
        {
            return FindEntityById<Storage, int>(id);
        }

        public IEnumerable<Storage> GetAll()
        {
            return GetAllWithEagerLoad<Storage>();
        }
    }
}
