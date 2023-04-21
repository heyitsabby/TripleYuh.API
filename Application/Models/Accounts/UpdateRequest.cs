namespace Application.Models.Accounts
{
    public class UpdateRequest
    {
        private string _password;
        private string _confirmPassword;
        private string _role;
        private string _email;

        public string Username { get; set; } = string.Empty;

        public string Role
        {
            get => _role;
            set => _role = ReplaceEmptyWithNull(value);
        }

        public string Email
        {
            get => _email;
            set => _email = ReplaceEmptyWithNull(value);
        }

        public string Password
        {
            get => _password;
            set => _password = ReplaceEmptyWithNull(value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = ReplaceEmptyWithNull(value);
        }

        // helpers

        private static string ReplaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
