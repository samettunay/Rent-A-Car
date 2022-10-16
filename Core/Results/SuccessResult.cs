using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(bool success, string message) : base(success, message)
        {
        }
        public SuccessResult(bool success) : base(success)
        {
        }
    }
}
