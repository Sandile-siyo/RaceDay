namespace RaceDay.MVC.Models
{
    // Model used to store Event data received from the API
    public class Event
    {
        // Event Id
        public int EventId { get; set; }

        // Event Name
        public string EventName { get; set; } = string.Empty;

        // Event Description
        public string EventDescription { get; set; } = string.Empty;

        // Event Date
        public DateTime EventDate { get; set; }

        // Event Location
        public string Location { get; set; } = string.Empty;

        // Event Distance
        public double Distance { get; set; }

        // Event Type
        public string EventType { get; set; } = string.Empty;
    }
}