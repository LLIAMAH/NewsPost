namespace NewsPost.Data.Reps
{
    public class Result<T> : IResult<T>
    {
        public string Error { get; private set; }
        public T Data { get; private set; }

        public Result(string message)
        {
            this.Error = message;
            this.Data = default;
        }

        public Result(T data)
        {
            this.Data = data;
            this.Error = string.Empty;
        }
    }

    public class ResultBool : Result<bool>
    {
        public ResultBool(string message) : base(message) { }

        public ResultBool(bool data) : base(data) { }
    }
}
