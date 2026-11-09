namespace RaceDay.MVC.Models
{
    // Model used to store registration data from the API
    public class Registration
    {
        // Registration Id
        public int RegistrationId { get; set; }

        // Registration Status
        public string RegistrationStatus { get; set; } = string.Empty;

        // Registration Date
        public DateTime RegistrationDate { get; set; }

        // Event Id
        public int EventId { get; set; }

        // User Id
        public int UserId { get; set; }

        // Category Id
        public int CategoryId { get; set; }
    }
}