using System.Collections.Generic;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface IAccountsRepository
    {
        void Add(Account entity, int userId, int storageId);
        void Update(Account entity);
        void Delete(int id);

        Account FindById(int id);
        IEnumerable<Account> GetAll();
        bool IsLoginAlreadyExist(string login);
        Account FindByLogin(string login);
    }
}
