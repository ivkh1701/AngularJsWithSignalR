using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface ICountryService
    {
        Task InsertCountryAsync(Country country);
        Task DeleteCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task<Country> GetCountryById(int country);
    }
}
