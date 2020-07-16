using System;
using System.Collections.Generic;
using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(FilesStorageContext context, Action<Exception> commandFailure) 
            : base(context, commandFailure)
        {

        }

        public User Add(User entity)
        {
            User added = null;
            Command(c =>
            {
                added = c.Set<User>().Add(entity);
            });
            return added;
        }

        public void Delete(int id)
        {
            DeleteEntity<User, int>(id);
        }

        public void Update(User entity)
        {
            CustomEntityUpdate(entity, entity.Id,
                u => u.FirstName, u => u.LastName, u => u.Email, u => u.Birthday, u => u.Gender);
        }

        public User FindById(int id)
        {
            return FindEntityById<User, int>(id);
        }

        public IEnumerable<User> GetAll()
        {
            return GetAllWithEagerLoad<User>();
        }
    }
}
