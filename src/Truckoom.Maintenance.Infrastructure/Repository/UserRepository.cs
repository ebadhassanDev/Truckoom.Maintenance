namespace Truckoom.Maintenance.Infrastructure.Repository;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truckoom.Maintenance.Core.Interfaces;
using Truckoom.Maintenance.Core.Models;

public class UserRepository : IUserRepository
{
    private readonly TruckoomDbContext _repository;
    public async Task AddUserAsync(User user)
    {
        _ = this._repository.User.Add(user);
        _ = await this._repository.SaveChangesAsync();
    }
    public async Task<User> GetUsernameAsync(string username) => await this._repository.User.FirstOrDefaultAsync(u => u.Username == username);
}
