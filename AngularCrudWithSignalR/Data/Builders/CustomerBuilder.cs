using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class CustomerBuilder
    {
        public CustomerBuilder(EntityTypeBuilder<Customer> customerBuilder)
        {
            customerBuilder.HasKey(x => x.Id);
            customerBuilder.Property(x => x.Email).IsRequired();
            customerBuilder.Property(x=>x.PassWord).IsRequired();   
        }
    }
}

