using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.Repositories
{
    public class TagsRepository : BaseRepository, ITagsRepository
    {
        public TagsRepository(FilesStorageContext context, Action<Exception> commandFailure) : base(context, commandFailure)
        {
        }

        public void Add(StorageTag entity, int storageId)
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
            DeleteEntity<StorageTag, int>(id);
        }

        public void Update(StorageTag entity)
        {
            UpdateEntity(entity);
        }

        public StorageTag FindById(int id)
        {
            return FindEntityById<StorageTag, int>(id);
        }

        public IEnumerable<StorageTag> GetAll(int storageId)
        {
            return FindByWithEagerLoad<StorageTag>(t => t.Storage.Id == storageId);
        }
    }
}
