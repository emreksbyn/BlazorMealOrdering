namespace BlazorMealOrdering.Shared.Dtos.Base
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}