namespace TwoFactorAuth.Domain.Core
{
    public class Result<TStatus, TValue>
    {
        public TStatus Status { get; set; }
        public TValue Value { get; set; }

        public Result(TStatus status, TValue value = default(TValue))
        {
            Status = status;
            Value = value;
        }
    }

    public static class Result
    {
        public static Result<T1, TValue> Create<T1, TValue>(T1 item1, TValue item2)
        {
            return new Result<T1, TValue>(item1, item2);
        }
    }
}