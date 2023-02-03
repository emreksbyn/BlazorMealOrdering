using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}