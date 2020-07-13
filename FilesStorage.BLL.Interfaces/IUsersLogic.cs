using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL.Interfaces
{
    public interface IUsersLogic
    {
        void AddUserProfile(UserSignUpDto createUser);
        void UpdateUserProfile(UserSignUpDto updatingUserDto);

        bool IsUserLoginExist(string login);
        bool IsUserLogDataValid(UserSignInDto userLog, out UserDto userDto);
        
        UserDto FindUserByLogin(string login);

    }
}
