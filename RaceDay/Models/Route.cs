namespace RaceDay.Models
{
    public class Route
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; } = string.Empty;

        public string RouteDescription { get; set; } = string.Empty;

        public int EventId { get; set; }
    }
}
