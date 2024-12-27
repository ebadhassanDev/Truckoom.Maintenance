namespace Truckoom.Maintenance.Core.Interfaces;

using Truckoom.Maintenance.Core.Models;

public interface IAuthService
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="userDto">The desired username for the new user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a boolean value indicating whether the signup was successful.</returns>
    Task<bool> SignupAsync(UserDto userDto);
    /// <summary>
    /// Authenticates a user asynchronously.
    /// </summary>
    /// <param name="username">The username of the user attempting to sign in.</param>
    /// <param name="password">The password of the user attempting to sign in.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a User object if authentication is successful, or null if authentication fails.</returns>
    Task<User?> SignInAsync(string username, string password);
}