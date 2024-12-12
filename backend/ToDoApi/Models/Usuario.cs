namespace ToDoApi.Models
{
    public class User 
    {
        public User() { }
        public User(string userName, string password)
        {
            UserName = userName;
            SetPasswordHash(password);
        }
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public string SetPasswordHash(string password) =>
                Password = BCrypt.Net.BCrypt.HashPassword(password);
        public static bool VerifyPasswordHash(string password, string passwordHash) =>
                BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
