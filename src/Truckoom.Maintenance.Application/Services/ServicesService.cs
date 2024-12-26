namespace Truckoom.Maintenance.Application;

using Truckoom.Maintenance.Core;
public class ServicesService(IServiceRepository service)
{
    private readonly IServiceRepository _repository = service;
    public Task<IEnumerable<Service>> GetAllServicesAsync() => _repository.GetAllServicesAsync();
    public Task<Service> GetServiceByIdAsync(int id) =>_repository.GetServiceByIdAsync(id);
    public Task AddServiceAsync(Service service )=> _repository.AddServiceAsync(service);
    public Task UpdateServiceAsync(Service service) => _repository.UpdateServiceAsync(service);
    public Task DeleteServiceAsync(int id) => _repository.DeleteServiceAsync(id);
}