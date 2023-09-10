using Microsoft.EntityFrameworkCore;
using UserManagementAPI.DTO;

namespace UserManagementAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User?> GetSingleUser(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id) ?? throw new Exception("User does not Exist.");

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> AddUser(UserDto userDto)
        {
            try
            {
                if (userDto == null) throw new Exception("Missing User.");

                var newUser = new User()
                {
                    FirstName = userDto.FirstName ?? throw new Exception("Missing Fields."),
                    LastName = userDto.LastName ?? throw new Exception("Missing Fields."),
                    Username = userDto.Username ?? throw new Exception("Missing Fields."),
                    ContactNo = userDto.ContactNo,
                    Place = userDto.Place
                };

                var user = await _dbContext.Users.Where(user =>
                    user.Username == newUser.Username
                ).FirstOrDefaultAsync();
                
                if (user != null) throw new Exception("User already exists!");

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return await _dbContext.Users.Where(u =>
                    u.Username == newUser.Username
                ).FirstOrDefaultAsync() ?? throw new Exception("Something went Wrong!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User?> UpdateUser(int id, UserDto updatedUser)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id) ?? throw new Exception("User does not Exist.");

                user.FirstName = string.IsNullOrEmpty(updatedUser.FirstName) ? user.FirstName : updatedUser.FirstName;
                user.LastName = string.IsNullOrEmpty(updatedUser.LastName) ? user.LastName : updatedUser.LastName;
                user.Username = string.IsNullOrEmpty(updatedUser.Username) ? user.Username : updatedUser.Username;
                user.Place = string.IsNullOrEmpty(updatedUser.Place) ? user.Place : updatedUser.Place;
                user.ContactNo = string.IsNullOrEmpty(updatedUser.ContactNo) ? user.ContactNo : updatedUser.ContactNo;

                await _dbContext.SaveChangesAsync();

                return await _dbContext.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User?> DeleteUser(int id)
        {
            try
            {
                var hero = await _dbContext.Users.FindAsync(id) ?? throw new Exception("User does not Exist.");

                _dbContext.Users.Remove(hero);
                await _dbContext.SaveChangesAsync();

                return hero;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

