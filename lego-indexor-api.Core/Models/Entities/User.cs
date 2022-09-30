namespace lego_indexor_api.Core.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public byte[]? Password { get; set; }
    }
}
