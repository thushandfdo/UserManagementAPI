using UserManagementAPI.DTO;

namespace UserManagementAPI.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User?> GetSingleUser(int id);

        Task<User> AddUser(UserDto user);

        Task<User?> UpdateUser(int id, UserDto updatedUser);

        Task<User?> DeleteUser(int id);
    }
}
