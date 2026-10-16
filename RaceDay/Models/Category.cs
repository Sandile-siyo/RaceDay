namespace RaceDay.Models
{//start of the namespace
    public class Category
    {//start of the class
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public double DistanceKM { get; set; }

        public string AgeGroup { get; set; } = string.Empty;

        public int EventId { get; set; }
    }//end of the class
}//end of the namespace