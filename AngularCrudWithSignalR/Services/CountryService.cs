using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;

namespace AngularCrudWithSignalR.Services
{
    public class CountryService : ICountryService
    {
        #region fields

        private readonly IRepository<Country> _CountryRepository;

        #endregion

        #region ctor

        public CountryService(IRepository<Country> CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }

        #endregion

        #region methods

        public async Task DeleteCountryAsync(Country country)
        {
            await _CountryRepository.DeleteAsync(country);
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            if (countryId < 0)
                throw new ArgumentOutOfRangeException(nameof(countryId));

            return await _CountryRepository.GetByIdAsync(countryId);
        }

        public async Task InsertCountryAsync(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            await _CountryRepository.InsertAsync(country);
        }

        public async Task UpdateCountryAsync(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            await _CountryRepository.UpdateAsync(country);
        }

        #endregion
    }
}
