using ProductAPI.Models;

namespace ProductAPI.Helpers
{
    public class ResponseHelper : IResponseHelper
    {
        public ApiResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, string message, T? data)
        {
            return new ApiResponse<T>
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        public ApiResponseWithToken<T> CreateResponseWithToken<T>(bool isSuccess, int statusCode, string message, T? data, string token)
        {
            return new ApiResponseWithToken<T>
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Token = token
            };
        }
    }

    public interface IResponseHelper
    {
        ApiResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, string message, T? data);
        ApiResponseWithToken<T> CreateResponseWithToken<T>(bool isSuccess, int statusCode, string message, T? data, string token);
    }
}
