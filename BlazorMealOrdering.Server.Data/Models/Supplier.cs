using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class Supplier : BaseModel
    {
        public string Name { get; set; }
        public string WebURL { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}