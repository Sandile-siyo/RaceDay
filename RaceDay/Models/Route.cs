namespace RaceDay.Models
{//start of the namespace
    public class Route
    {//start of the class
        public int RouteId { get; set; }

        public string RouteName { get; set; } = string.Empty;

        public string RouteDescription { get; set; } = string.Empty;

        public int EventId { get; set; }
    }//end of the class
}//end of the namespace
