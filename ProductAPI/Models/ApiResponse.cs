namespace ProductAPI.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public class ApiResponseWithToken<T> : ApiResponse<T>
    {
        public string Token { get; set; } = string.Empty;
    }
}
