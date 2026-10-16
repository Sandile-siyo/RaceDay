namespace RaceDay.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        public TimeSpan FinishTime { get; set; }

        public int OverallPosition { get; set; }

        public int CategoryPosition { get; set; }

        public string Pace { get; set; } = string.Empty;

        public string ResultStatus { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int EventId { get; set; }
    }
}
