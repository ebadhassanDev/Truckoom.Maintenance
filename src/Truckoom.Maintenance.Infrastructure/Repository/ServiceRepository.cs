namespace Truckoom.Maintenance.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truckoom.Maintenance.Core;

internal class ServiceRepository : IServiceRepository
{
    private readonly TruckoomDbContext _repository;
    public async Task AddServiceAsync(Service service)
    {
        _ = this._repository.Services.Add(service);
        _ = await this._repository.SaveChangesAsync();
    }
    public async Task DeleteServiceAsync(int id)
    {
        var service = await this._repository.Services.Include(s => s.Tasks).FirstOrDefaultAsync(s => s.ServiceId == id);
        if (service != null)
        {
            _ = this._repository.Services.Remove(service);
            _ = await this._repository.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Service>> GetAllServicesAsync() => await this._repository.Services.Include(s => s.Tasks).ToListAsync();
    public async Task<Service> GetServiceByIdAsync(int id) => await this._repository.Services.Include(s => s.Tasks).FirstOrDefaultAsync(s => s.ServiceId == id);
    public async Task UpdateServiceAsync(Service service)
    {
        _ = this._repository.Entry(service).State = EntityState.Modified;
        _ = await this._repository.SaveChangesAsync();
    }
}
