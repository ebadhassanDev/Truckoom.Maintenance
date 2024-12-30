namespace Truckoom.Maintenance.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truckoom.Maintenance.Core.Models;

internal sealed class ServiceRepository(TruckoomDbContext truckoomDb) : IServiceRepository
{
    private readonly TruckoomDbContext _repository = truckoomDb;
    public async Task AddServiceAsync(Service service)
    {
        _ = this._repository.Services.Add(service);
        _ = await this._repository.SaveChangesAsync();
    }
    public async Task DeleteServiceAsync(int id)
    {
        var service = await this._repository.Services.Include(s => s.ServiceTasks).FirstOrDefaultAsync(s => s.ServiceId == id).ConfigureAwait(false);
        if (service != null)
        {
            _ = this._repository.Services.Remove(service);
            _ = await this._repository.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Service>> GetAllServicesAsync() => await this._repository.Services.Include(s => s.ServiceTasks).ToListAsync().ConfigureAwait(false);
    public async Task<Service> GetServiceByIdAsync(int id) => await this._repository.Services.Include(s => s.ServiceTasks).FirstOrDefaultAsync(s => s.ServiceId == id).ConfigureAwait(false);
    public async Task UpdateServiceAsync(Service service)
    {
        _ = this._repository.Entry(service).State = EntityState.Modified;
        _ = await this._repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
