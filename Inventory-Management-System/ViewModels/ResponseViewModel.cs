namespace Inventory_Management_System.ViewModels
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode ErrorCode { get; set; }


        public static SuccessResponseViewModel<T> Success(T data, string message = "")
        {
            return new SuccessResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                ErrorCode = ErrorCode.None
            };
        }

        public static ErrorResponseViewModel Error(ErrorCode errorCode, string message = "")
        {
            return new ErrorResponseViewModel
            {
                Data = default,
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode
            };

        }


    }
    public class SuccessResponseViewModel<T> : ResponseViewModel<T>
    {

    }

    public class ErrorResponseViewModel : ResponseViewModel<bool>
    {

    }
}
