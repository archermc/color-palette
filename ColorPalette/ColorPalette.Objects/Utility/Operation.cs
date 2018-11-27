using System;

namespace ColorPalette.Objects.Utility
{
    public class Operation
    {
        public bool DidSucceed { get; set; }
        public Exception Exception { get; set; }

        protected Operation(bool didSucceed, Exception exception = null)
        {
            DidSucceed = didSucceed;
            Exception = exception;
        }

        public static Operation WithSuccess()
        {
            return new Operation(true);
        }
    }

    public class Operation<T> : Operation
    {
        public T Result { get; set; }

        public static Operation<T> WithSuccess(T result)
        {
            return new Operation<T>(true, result);
        }

        public static Operation<T> WithException(Exception exception)
        {
            return new Operation<T>(false, default(T), exception);
        }

        public static Operation<T> FromExisting(Operation operation)
        {
            return new Operation<T>(operation.DidSucceed, default(T), operation.Exception);
        }

        private Operation(bool didSucceed, T result = default(T), Exception exception = null) : base(didSucceed, exception)
        {
            Result = result;
        }
    }
}
