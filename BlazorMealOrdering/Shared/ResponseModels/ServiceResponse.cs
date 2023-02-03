using BlazorMealOrdering.Shared.ResponseModels.Base;

namespace BlazorMealOrdering.Shared.ResponseModels
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Value { get; set; }
    }
}