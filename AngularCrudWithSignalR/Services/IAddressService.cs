using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface IAddressService
    {
        Task InsertAddressAsync(Address address);
        Task DeleteAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task<Address> GetAddressById(int addressId);
        Task<Address> GetAddressByCustomerId(int customerId);
    }
}
