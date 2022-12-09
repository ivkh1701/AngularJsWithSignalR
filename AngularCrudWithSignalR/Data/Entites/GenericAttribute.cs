using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCrudWithSignalR.Data.Entites
{

    public class GenericAttribute : BaseEntity
    {
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
