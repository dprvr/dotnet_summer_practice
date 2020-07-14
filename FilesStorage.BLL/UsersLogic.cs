using System;
using System.Linq;
using FilesStorage.BLL.Interfaces;
using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;
using FilesStorage.Entities.Mappers;

namespace FilesStorage.BLL
{
    public class UsersLogic : IUsersLogic
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IBLMapper _mapper;
        private readonly IHasher _hasher;

        public UsersLogic(IAccountsRepository accountsRepository, IUsersRepository usersRepository,
            IStorageRepository storageRepository, IBLMapper mapper, IHasher hasher)
        {
            _accountsRepository = accountsRepository;
            _usersRepository = usersRepository;
            _storageRepository = storageRepository;
            _mapper = mapper;
            _hasher = hasher;
        }

        public void AddUserProfile(UserSignUpDto createUser)
        {
            var user = _mapper.Map<User, UserSignUpDto>(createUser);
            var account = _mapper.Map<Account, UserSignUpDto>(createUser);
            
            var addedUser = _usersRepository.Add(user);
            var addedStorage = _storageRepository.Add(new Storage());

            account.HashedPassword = _hasher.Hash(createUser.Password);

            _accountsRepository.Add(account, addedUser.Id, addedStorage.Id);
        }

        public void UpdateUserProfile(UserSignUpDto updatingUserDto)
        {
            var account = _mapper.Map<Account, UserSignUpDto>(updatingUserDto);
            var user = _mapper.Map<User, UserSignUpDto>(updatingUserDto);

            var found = _accountsRepository.FindById(updatingUserDto.Id);
            user.Id = found.UserId;

            if (!String.IsNullOrEmpty(updatingUserDto.Password))
            {
                account.HashedPassword = _hasher.Hash(updatingUserDto.Password);
            }
            _accountsRepository.Update(account);
            _usersRepository.Update(user);
        }

        public bool IsUserLoginExist(string login)
        {
            return _accountsRepository.IsLoginAlreadyExist(login);
        }

        public bool IsUserLogDataValid(UserSignInDto userLog, out UserDto userDto)
        {            
            userDto = null;
            var realUserLog = _accountsRepository.FindByLogin(userLog.Login);
            if (realUserLog == null) return false;
            if (realUserLog.HashedPassword == _hasher.Hash(userLog.Password))
            {
                userDto = _mapper.Map<UserDto, Account>(realUserLog);
                return true;
            }
            return false;
        }

        public UserDto FindUserByLogin(string login)
        {
            var foundAccount = _accountsRepository.FindByLogin(login);
            var foundUser = _usersRepository.FindById(foundAccount.UserId);
            var fullDto = new UserDto
            {
                FirstName = foundUser.FirstName,
                LastName = foundUser.LastName,
                Birthday = foundUser.Birthday,
                Email = foundUser.Email,
                Gender = foundUser.Gender,
                Login = foundAccount.Login,
                Id = foundAccount.Id
            };
            return fullDto;
        }
    }
}
