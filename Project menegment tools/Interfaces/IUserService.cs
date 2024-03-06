using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Users;

namespace Project_menegment_tools.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Create New User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    ValueTask<UserViewModel> CreateAsync(UserCreationModel user);

    /// <summary>
    /// Update exist user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <param name="isUsedDeleted"></param>
    /// <returns></returns>
    ValueTask<UserViewModel> UpdateAsync(long  id, UserUpdateModel user, bool isUsedDeleted);

    /// <summary>
    /// Delete exist user via 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Search for user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<UserViewModel> GetByIdAsync(long id);

    /// <summary>
    /// User search by password and email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    ValueTask<bool> GetByEmailPassword(string email, string password, Role role);

    /// <summary>
    /// View all registered users
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
}
