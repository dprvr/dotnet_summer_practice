using System;
using System.Collections.Generic;
using System.Linq;

using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.Repositories
{
    public class AccountsRepository : BaseRepository, IAccountsRepository
    {
        public AccountsRepository(FilesStorageContext context, Action<Exception> commandFailure) 
            : base(context, commandFailure)
        {

        }
        
        public bool IsLoginAlreadyExist(string login)
        {
            return Query<Account>().Any(l => l.Login == login);
        }

        public Account FindByLogin(string login)
        {
            return Query<Account>().FirstOrDefault(a => a.Login == login);
        }

        public void Add(Account entity, int userId, int storageId)
        {
            Command(c =>
            {
                entity.CreationDate = DateTime.Now;
                entity.UserId = userId;
                entity.StorageId = storageId;
                c.Entry(entity).State = System.Data.Entity.EntityState.Added;
            });
        }

        public void Update(Account updatedEntity)
        {
            var entity = FindEntityById<Account, int>(updatedEntity.Id, false);
            entity.HashedPassword = (String.IsNullOrEmpty(updatedEntity.HashedPassword)) ? entity.HashedPassword
                : updatedEntity.HashedPassword;
            UpdateEntity(entity);
        }

        public void Delete(int id)
        {
            DeleteEntity<Account, int>(id);
        }

        public Account FindById(int id)
        {
            return FindEntityById<Account, int>(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return GetAllWithEagerLoad<Account>();
        }
    }
}
