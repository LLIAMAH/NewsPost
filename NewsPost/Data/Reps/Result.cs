namespace NewsPost.Data.Reps
{
    public class Result<T> : IResult<T>
    {
        public string Error { get; private set; }
        public T Data { get; private set; }

        public Result(string error)
        {
            this.Error = error;
            this.Data = default;
        }

        public Result(T data)
        {
            this.Error = string.Empty;
            this.Data = data;
        }
    }

    public class ResultBool : Result<bool>
    {
        public ResultBool(string error) : base(error) { }

        public ResultBool(bool data) : base(data) { }
    }
}
