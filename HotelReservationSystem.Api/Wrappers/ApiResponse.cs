using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelReservationSystem.Api.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public ApiResponse(bool success, string? message, T? data, List<string>? errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }
        public static ApiResponse<T> Ok(string? message = null, T? data = default)
        {
            return new ApiResponse<T>(true, message, data, null);
        }
        public static ApiResponse<T> Fail(string? message = null,List<string>? errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(false, message, default, new List<string> { message });
        }
    }
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public ApiResponse(bool success, string? message, List<string>? errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }
        public static ApiResponse Ok(string? message = null)
        {
            return new ApiResponse(true, message,null);
        }
        public static ApiResponse Fail(string? message = null, List<string>? errors = null)
        {
            return new ApiResponse(false, message, errors);
        }
        public static ApiResponse Fail(string message)
        {
            return new ApiResponse(false, message, new List<string> { message });
        }
    }
}
