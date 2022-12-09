using AngularCrudWithSignalR.Models;

namespace AngularCrudWithSignalR.Factories
{
    public interface ICustomerfactory
    {
        Task<IList<CustomerModel>> PrepareAllCustomer();
        Task AddCustomerAsync(CustomerModel model);
        Task DeleteCustomerAsync(int id);
        Task UpdateCustomerAsync(CustomerModel model);
        Task<CustomerModel> PrepareCustomerById(int customerId);
    }
}
