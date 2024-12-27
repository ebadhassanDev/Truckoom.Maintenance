namespace Truckoom.Maintenance.Core.Models;
/// <summary>
/// Defines an interface for a repository that manages Service entities.
/// </summary>
public interface IServiceRepository
{
    /// <summary>
    /// Retrieves all services asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of Service objects.</returns>
    Task<IEnumerable<Service>> GetAllServicesAsync();
    /// <summary>
    /// Retrieves a service by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the service to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the Service object with the specified ID, or null if not found.</returns>
    Task<Service> GetServiceByIdAsync(int id);
    /// <summary>
    /// Adds a new service asynchronously.
    /// </summary>
    /// <param name="service">The Service object to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddServiceAsync(Service service);
    /// <summary>
    /// Updates an existing service asynchronously.
    /// </summary>
    /// <param name="service">The Service object with updated information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateServiceAsync(Service service);
    /// <summary>
    /// Deletes a service by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the service to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteServiceAsync(int id);
}