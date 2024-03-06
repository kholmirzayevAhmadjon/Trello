using Project_menegment_tools.Enums;
using Project_menegment_tools.Extentions;
using Project_menegment_tools.Helps;
using Project_menegment_tools.Interfaces;
using Project_menegment_tools.Models.User;
using Project_menegment_tools.Models.Users;

namespace Project_menegment_tools.Services;

public class UserService : IUserService
{
    private List<User> users;

    public async ValueTask<UserViewModel> CreateAsync(UserCreationModel user)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);

        var existUser = users.FirstOrDefault(u => u.Email == user.Email);
        //If model is exist and was deleted with this email , update this model
        if (existUser is not null)
        {
            if (existUser.IsDeleted)
            {
                // Recover deleted model with this email
                return await UpdateAsync(existUser.Id, user.ToMapped(), true);
            }
            throw new Exception($"This user is already exist with this email={user.Email}");
        }

        var createUser = users.Create(user.ToMap());
        await FileIO.WriteAsync(Constants.USER_PATH, users);

        return createUser.ToMap();

    }


    public async ValueTask<bool> DeleteAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        existUser.IsDeleted = true;
        existUser.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_PATH, users);
        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        return users.Where(u => !u.IsDeleted).ToList().ToMap();
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        return existUser.ToMap();
    }

    public async ValueTask<bool> GetByEmailPassword(string email, string password, Role role)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == role && !u.IsDeleted);
        if(existUser == null)
        {
            return false;
        }


        return true;
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsedDeleted = false)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = new User();

        if (isUsedDeleted)
        {
            existUser = users.FirstOrDefault(users => users.Id == id );
            existUser.IsDeleted = false;
        }
        else
        {
            existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This user is not found with ID = {id}");
        }
        existUser.Id = id;
        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;
        existUser.Password = user.Password;
        existUser.Role = user.Role;
        existUser.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.USER_PATH, users);

        return existUser.ToMap();
    }

}
