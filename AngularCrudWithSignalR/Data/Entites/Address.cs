using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCrudWithSignalR.Data.Entites
{
    public class Address : BaseEntity
    {
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [ForeignKey("StateId")]
        public int StateId { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
    }
}
