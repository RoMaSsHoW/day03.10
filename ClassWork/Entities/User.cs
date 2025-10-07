using ClassWork.Entities.ValueObjects;

namespace ClassWork.Entities
{
    public class User
    {
        public User() { }

        private User(
            string name,
            Email email,
            Password password,
            string refreshToken)
        {
            Name = name;
            Email = email;
            Password = password;
            RefreshToken = refreshToken;
        }

        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public string RefreshToken { get; private set; } = string.Empty;


        public static User Registr(
            string name,
            string email,
            string password,
            string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Refresh token cannot be empty", nameof(refreshToken));

            var emailVO = Email.Create(email);
            var passwordVO = Password.Create(password);
            return new User(name, emailVO, passwordVO, refreshToken);
        }

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password.PasswordHash);
        }

        public void ChangeRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("RefreshToken cannot be null or whitespace.");

            RefreshToken = refreshToken;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty.", nameof(newName));

            Name = newName;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty.", nameof(newEmail));

            Email = Email.Create(newEmail);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!Verify(currentPassword))
                throw new ArgumentException("Current password is incorrect.", nameof(currentPassword));

            Password = Password.Create(newPassword);
        }
    }
}
