namespace NewsApp.Shared
{
    /// <summary>
    /// Class that represents some operation result. Success it or not
    /// </summary>
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
