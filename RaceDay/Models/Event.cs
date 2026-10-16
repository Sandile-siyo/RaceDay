namespace RaceDay.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public double Distance { get; set; }

        public string EventType { get; set; } = string.Empty;
    }
}