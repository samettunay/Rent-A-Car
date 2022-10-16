using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, bool success, string messages) : base(data, true, messages)
        {
        }
        public SuccessDataResult(T data, bool success) : base(data, true)
        {
        }
        public SuccessDataResult(T data) : base(data, true)
        {

        }
        public SuccessDataResult() : base(default,true)
        {

        }
    }
}
