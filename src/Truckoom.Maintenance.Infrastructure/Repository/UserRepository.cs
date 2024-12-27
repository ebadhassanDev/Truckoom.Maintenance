namespace Truckoom.Maintenance.Infrastructure.Repository;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truckoom.Maintenance.Core.Interfaces;
using Truckoom.Maintenance.Core.Models;

public class UserRepository(TruckoomDbContext truckoomDb) : IUserRepository
{
    private readonly TruckoomDbContext _repository = truckoomDb;
    public async Task AddUserAsync(User user)
    {
        _ = this._repository.Users.Add(user);
        _ = await this._repository.SaveChangesAsync();
    }
    public async Task<User> GetUsernameAsync(string username) => await this._repository.Users.FirstOrDefaultAsync(u => u.Username == username);
}
