using AngularCrudWithSignalR.Data.Builders;
using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace AngularCrudWithSignalR.Data.DbCon
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new CountryBuilder(builder.Entity<Country>());
            new Statebuilder(builder.Entity<State>());
            new Addressbuilder(builder.Entity<Address>());
            new GenericAttributeBuilder(builder.Entity<GenericAttribute>());
            new CustomerBuilder(builder.Entity<Customer>());
            new DownloadBuilder(builder.Entity<Download>());
        }
    }
}
