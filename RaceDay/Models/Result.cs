namespace RaceDay.Models
{//start of the namespace
    public class Result
    {//start of the class
        public int ResultId { get; set; }

        public TimeSpan FinishTime { get; set; }

        public int OverallPosition { get; set; }

        public int CategoryPosition { get; set; }

        public string Pace { get; set; } = string.Empty;

        public string ResultStatus { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int EventId { get; set; }
    }//end of the class
}//end of the namespace
