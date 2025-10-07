namespace ClassWork.Entities.ValueObjects
{
    public class Email
    {
        public Email() { }

        private Email(string emailAddress)
        {
            EmailAddress = emailAddress.ToLower();
        }

        public string EmailAddress { get; }

        public static Email Create(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains("@"))
            {
                throw new ArgumentException("Invalid email address", nameof(emailAddress));
            }

            return new Email(emailAddress);
        }
    }
}
