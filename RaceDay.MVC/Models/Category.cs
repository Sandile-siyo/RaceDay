namespace RaceDay.MVC.Models
{
    // Model used to store Category data received from the API
    public class Category
    {
        // Category Id
        public int CategoryId { get; set; }

        // Category Name
        public string CategoryName { get; set; } = string.Empty;

        // Distance
        public double Distance { get; set; }
    }
}
