namespace Truckoom.Maintenance.Core.Interfaces;

using Truckoom.Maintenance.Core.Models;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their username asynchronously.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a User object if found; otherwise, it may return null or throw an exception depending on the implementation.</returns>
    Task<User> GetUsernameAsync(string username);
    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="user">The User object to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddUserAsync(User user);
}