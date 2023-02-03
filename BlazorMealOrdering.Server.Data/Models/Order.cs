using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class Order : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }

        public Guid CreateUserId { get; set; }
        public virtual User User { get; set; }

        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}