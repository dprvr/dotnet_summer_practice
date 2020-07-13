using System.Collections.Generic;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.Interfaces
{
    public interface IUsersRepository
    {
        User Add(User entity);
        void Delete(int id);
        void Update(User entity);

        User FindById(int id);
        IEnumerable<User> GetAll();
    }
}
