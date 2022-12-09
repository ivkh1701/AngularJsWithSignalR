namespace AngularCrudWithSignalR.Data.Entites
{
    public class Download :BaseEntity
    {
        public byte[] BinaryData { get; set; }
        public string Mimetype { get; set; }

        public string FileName { get; set; }
    }
}
