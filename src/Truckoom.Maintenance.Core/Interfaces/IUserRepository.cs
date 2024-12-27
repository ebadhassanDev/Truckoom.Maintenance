namespace Truckoom.Maintenance.Core.Interfaces;

using Truckoom.Maintenance.Core.Models;

public interface IUserRepository
{
    Task<User> GetUsernameAsync(string username);
    Task AddUserAsync(User user);
}