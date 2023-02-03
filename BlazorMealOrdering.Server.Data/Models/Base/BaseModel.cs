namespace BlazorMealOrdering.Server.Data.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}