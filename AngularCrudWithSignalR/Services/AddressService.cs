using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AngularCrudWithSignalR.Services
{
    public partial class AddressService : IAddressService
    {

        #region fields

        private readonly IRepository<Address> _addressRepository;

        #endregion

        #region ctor

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        #endregion

        #region methods

        public async Task DeleteAddressAsync(Address address)
        {
            await _addressRepository.DeleteAsync(address);
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            if (addressId < 0)
                throw new ArgumentOutOfRangeException(nameof(addressId));

            return await _addressRepository.GetByIdAsync(addressId);
        }

        public async Task<Address> GetAddressByCustomerId(int customerId)
        {
            if (customerId < 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            var query = from a in _addressRepository.Table
                        where a.CustomerId == customerId
                        select a;

            return await Task.FromResult<Address>(query.FirstOrDefault());
        }

        public async Task InsertAddressAsync(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            await _addressRepository.InsertAsync(address);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            await _addressRepository.UpdateAsync(address);
        }

        #endregion

    }
}
