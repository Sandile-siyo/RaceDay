namespace RaceDay.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        public string RegistrationStatus { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
    }
}