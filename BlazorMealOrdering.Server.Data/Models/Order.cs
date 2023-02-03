using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class Order : BaseModel
    {
        public Guid CreateUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}