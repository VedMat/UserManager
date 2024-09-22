namespace UserManager.DTOs
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse() { }

        public ServiceResponse(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        // Factory methods for easier instantiation
        public static ServiceResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ServiceResponse<T>(data, true, message);
        }

        public static ServiceResponse<T> ErrorResponse(string message)
        {
            return new ServiceResponse<T>(default(T), false, message);
        }
    }

}
