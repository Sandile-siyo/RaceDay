namespace RaceDay.Models
{
    public class Weather
    {
        public int WeatherId { get; set; }

        public DateTime ForecastDate { get; set; }

        public string Conditions { get; set; } = string.Empty;

        public int EventId { get; set; }
    }
}
