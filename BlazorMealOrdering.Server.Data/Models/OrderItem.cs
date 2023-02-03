using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class OrderItem : BaseModel
    {
        public string Description { get; set; }

        public Guid CreateUserId { get; set; }
        public virtual User User { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}