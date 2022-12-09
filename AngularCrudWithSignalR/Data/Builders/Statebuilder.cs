using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class Statebuilder
    {
        public Statebuilder(EntityTypeBuilder<State> statebuilder)
        {
            statebuilder.HasKey(x => x.Id);
            statebuilder.Property(x => x.Name).IsRequired();
            statebuilder.Property(x => x.Code).IsRequired();
        }
    }
}
