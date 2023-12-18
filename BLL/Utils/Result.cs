namespace BLL.Utils
{
    public class Result<T>
        where T : class
    {
        private bool isError;

        private string message;

        public Result(bool _isError, string _mes = "", T? value = null)
        {
            isError = _isError;
            message = _mes;
            Value = value;
        }

        public bool IsError { get => isError; }

        public string Message { get => message; }

        public T? Value { get; set; }
    }
}
