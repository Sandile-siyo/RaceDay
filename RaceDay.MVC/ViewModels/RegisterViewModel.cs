namespace RaceDay.MVC.ViewModels
{
    // ViewModel used to capture registration information
    // from the user interface before sending it to the API
    public class RegisterViewModel
    {
        // User's first name
        public string FirstName { get; set; } = string.Empty;

        // User's last name
        public string LastName { get; set; } = string.Empty;

        // User's email address
        public string Email { get; set; } = string.Empty;

        // User's chosen password
        public string Password { get; set; } = string.Empty;

        // User role (Organiser or Participant)
        public string Role { get; set; } = string.Empty;
    }
}