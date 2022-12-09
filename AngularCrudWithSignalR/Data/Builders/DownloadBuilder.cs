using AngularCrudWithSignalR.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularCrudWithSignalR.Data.Builders
{
    public class DownloadBuilder
    {
        public DownloadBuilder(EntityTypeBuilder<Download> pictureBuilder)
        {
            pictureBuilder.HasKey(x => x.Id);
            pictureBuilder.Property(x => x.BinaryData).IsRequired();
        }
    }
}
