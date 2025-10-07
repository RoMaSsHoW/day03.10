namespace ClassWork.Entities.ValueObjects
{
    public class Password
    {
        public Password() { }

        private Password(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public string PasswordHash { get; }

        public static Password Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long", nameof(password));
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return new Password(passwordHash);
        }
    }
}
