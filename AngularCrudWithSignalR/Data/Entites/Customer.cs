namespace AngularCrudWithSignalR.Data.Entites
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int PictureId { get; set; }
    }
}
