using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;
using System.Data;

namespace AngularCrudWithSignalR.Services
{
    public class CustomerService : ICustomerService
    {
        #region fields

        private readonly IRepository<Customer> _cutomerRepository;

        #endregion

        #region ctor

        public CustomerService(IRepository<Customer> cutomerRepository)
        {
            _cutomerRepository = cutomerRepository;
        }

        #endregion

        #region methods

        public virtual async Task<List<Customer>> GetCustomersAsync()
        {
            var query = from c in _cutomerRepository.Table
                        select c;

            return await Task.FromResult<List<Customer>>(query.ToList());
        }

        public virtual async Task InsertCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new NoNullAllowedException();

            await _cutomerRepository.InsertAsync(customer);
        }

        public virtual async Task UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new NoNullAllowedException();

            await _cutomerRepository.UpdateAsync(customer);
        }

        public virtual async Task DeleteCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new NoNullAllowedException();

            await _cutomerRepository.DeleteAsync(customer);
        }

        public virtual async Task<Customer> GetCustomerById(int customerId)
        {
            if (customerId <= 0)
                throw new NoNullAllowedException();

            return await _cutomerRepository.GetByIdAsync(customerId);
        }

        #endregion
    }
}
