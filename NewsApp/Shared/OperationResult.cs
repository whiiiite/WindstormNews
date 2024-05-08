namespace NewsApp.Shared
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public static OperationResult IsNotSuccess(string message)
        {
            return new OperationResult()
            {
                IsSuccess = false,
                Message = message
            };
        }

        public static OperationResult SuccessInstance
        {
            get
            {
                return new OperationResult { IsSuccess = true };
            }
        }
    }
}
