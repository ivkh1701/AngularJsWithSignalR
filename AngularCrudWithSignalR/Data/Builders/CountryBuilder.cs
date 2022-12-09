using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class CountryBuilder
    {
        public CountryBuilder(EntityTypeBuilder<Country> countryBuilder)
        {
            countryBuilder.HasKey(x => x.Id);
            countryBuilder.Property(x => x.Name).IsRequired();
            countryBuilder.Property(x => x.CountryCode).IsRequired();
        }
    }
}
