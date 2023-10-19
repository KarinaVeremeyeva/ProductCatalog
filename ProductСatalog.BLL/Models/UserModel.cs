namespace ProductСatalog.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool IsLocked { get; set; }
    }
}
