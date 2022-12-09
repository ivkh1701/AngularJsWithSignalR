using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class GenericAttributeBuilder
    {
        public GenericAttributeBuilder(EntityTypeBuilder<GenericAttribute> genericAttributeBuilder)
        {
            genericAttributeBuilder.HasKey(x => x.Id);
            genericAttributeBuilder.Property(x => x.Key).IsRequired();
            genericAttributeBuilder.Property(x => x.Value).IsRequired();
            genericAttributeBuilder.Property(x => x.CustomerId).IsRequired();
        }
    }
}
