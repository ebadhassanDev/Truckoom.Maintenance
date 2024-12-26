namespace Truckoom.Maintenance.Application.Services;

using Truckoom.Maintenance.Core;
public class ServicesService(IServiceRepository service)
{
    private readonly IServiceRepository repository = service;
    public Task<IEnumerable<Service>> GetAllServicesAsync() => this.repository.GetAllServicesAsync();
    public Task<Service> GetServiceByIdAsync(int id) => this.repository.GetServiceByIdAsync(id);
    public Task AddServiceAsync(Service service) => this.repository.AddServiceAsync(service);
    public Task UpdateServiceAsync(Service service) => this.repository.UpdateServiceAsync(service);
    public Task DeleteServiceAsync(int id) => this.repository.DeleteServiceAsync(id);
}