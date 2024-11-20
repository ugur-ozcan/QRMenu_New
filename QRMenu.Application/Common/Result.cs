namespace QRMenu.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string Message { get; }
        public List<string> Errors { get; }

        protected Result(bool isSuccess, T data = default, string message = null, List<string> errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static Result<T> Success(T data, string message = null)
        {
            return new Result<T>(true, data, message);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, null, new List<string> { error });
        }

        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(false, default, null, errors);
        }
    }
}
