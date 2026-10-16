namespace RaceDay.Models
{//start of the namespace
    public class Weather
    {//start of the class
        public int WeatherId { get; set; }

        public DateTime ForecastDate { get; set; }

        public string Conditions { get; set; } = string.Empty;

        public int EventId { get; set; }
    }//end of the class
}//end of the namespace