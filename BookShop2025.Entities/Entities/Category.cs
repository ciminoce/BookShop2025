using System.ComponentModel.DataAnnotations;

namespace BookShop2025.Entities.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
