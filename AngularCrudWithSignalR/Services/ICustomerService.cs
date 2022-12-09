using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task InsertCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerById(int customerId);
        Task DeleteCustomerAsync(Customer customer);
    }
}
