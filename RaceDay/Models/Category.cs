namespace RaceDay.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public double DistanceKM { get; set; }

        public string AgeGroup { get; set; } = string.Empty;

        public int EventId { get; set; }
    }
}