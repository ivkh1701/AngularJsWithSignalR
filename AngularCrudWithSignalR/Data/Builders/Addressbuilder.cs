using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class Addressbuilder
    {
        public Addressbuilder(EntityTypeBuilder<Address> addressBuilder)
        {
            addressBuilder.HasKey(x => x.Id);
            addressBuilder.Property(x => x.AddressLine1).IsRequired();
            addressBuilder.Property(x => x.CustomerId).IsRequired();
            addressBuilder.Property(x => x.StateId).IsRequired();
            addressBuilder.Property(x => x.CountryId).IsRequired();

        }
    }
}
