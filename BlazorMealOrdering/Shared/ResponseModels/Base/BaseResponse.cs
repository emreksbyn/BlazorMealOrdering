namespace BlazorMealOrdering.Shared.ResponseModels.Base
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        public void SetException(Exception exception)
        {
            Success = false;
            Message = exception.Message;
        }
    }
}