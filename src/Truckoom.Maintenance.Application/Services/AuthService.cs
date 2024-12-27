namespace Truckoom.Maintenance.Application.Services;

using System.Security.Cryptography;
using System.Text;
using Truckoom.Maintenance.Core.Interfaces;
using Truckoom.Maintenance.Core.Models;

public class AuthService(IUserRepository repository)
{
    private readonly IUserRepository _userRepository = repository;
    public async Task<bool> SignupAsync(string userName, string email, string password)
    {
        if (await this._userRepository.GetUsernameAsync(userName) is not null)
        {
            return false;
        }
        var hashedPassword = HashPassword(password);
        User user = new()
        {
            Username = userName,
            Email = email,
            PasswordHash = hashedPassword
        };

        await this._userRepository.AddUserAsync(user);
        return true;
    }
    public async Task<User?> SignInAsync(string username, string password)
    {
        var user = await this._userRepository.GetUsernameAsync(username);
        if (user is null || !VerifyPassword(password, user.PasswordHash))
        {
            return default;
        }
        return user;
    }
    private static bool VerifyPassword(string password, string hasedPassword)
    {
        var hashedInput = HashPassword(password);
        return hashedInput == hasedPassword;
    }
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}