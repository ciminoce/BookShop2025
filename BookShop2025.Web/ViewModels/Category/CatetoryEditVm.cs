using System.ComponentModel;

namespace BookShop2025.Web.ViewModels.Category
{
    public class CatetoryEditVm
    {
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; } = null!;

    }
}
