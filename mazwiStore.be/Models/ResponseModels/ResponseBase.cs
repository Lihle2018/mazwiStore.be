namespace mazwiStore.be.Models.ResponseModels
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ResponseBase<T> SuccessResponse(T data, string message = "")
        {
            return new ResponseBase<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static ResponseBase<T> FailureResponse(string message)
        {
            return new ResponseBase<T>
            {
                Success = false,
                Data = default,
                Message = message
            };
        }
    }
}
