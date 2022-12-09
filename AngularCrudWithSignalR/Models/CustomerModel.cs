using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Models
{
    public class CustomerModel : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
    }
}
