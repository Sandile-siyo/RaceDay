namespace RaceDay.Models
{//start of the namespace
    public class Registration
    {//start of the class
        public int RegistrationId { get; set; }

        public string RegistrationStatus { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
    }//end of the class
}//end of the namespace