
using FilesStorage.BLL.Interfaces;
using FilesStorage.DAL.Interfaces;

namespace FilesStorage.BLL
{ 
    public class Conventions : IConventions
    {
        private readonly IAccountsRepository _accountsRepository;

        public Conventions(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public int GetStorageIdByUserLogin(string userLogin)
        {
            return _accountsRepository.FindByLogin(userLogin).StorageId;
        }

    }
}
