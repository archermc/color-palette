using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPalette.Objects.Utility
{
    public class Operation
    {
        public bool DidSucceed { get; set; }
        public Exception Exception { get; set; }
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

        private Operation(bool didSucceed, T result = default(T), Exception exception = null)
        {
            Result = result;
            DidSucceed = didSucceed;
            Exception = exception;
        }
    }
}
