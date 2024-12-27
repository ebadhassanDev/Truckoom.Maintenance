namespace Truckoom.Maintenance.Core.Interfaces;
public interface ITokenService
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for a given username and email.
    /// </summary>
    /// <param name="username">The username to include in the token payload.</param>
    /// <param name="userEmail">The user's email address to include in the token payload.</param>
    /// <returns>A string representing the generated JWT.</returns>
    string GenerateJwtToken(string username, string userEmail);
}
