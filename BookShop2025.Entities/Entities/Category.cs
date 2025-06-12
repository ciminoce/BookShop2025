using System.ComponentModel.DataAnnotations;

namespace BookShop2025.Entities.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="The name is required")]
        [StringLength(50,ErrorMessage ="Must have between {2} and {1} characters",MinimumLength =3)]
        public string CategoryName { get; set; } = null!;
    }
}
