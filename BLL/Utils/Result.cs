using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public class Result
    {
        private bool isError;
        private string message;

        public Result(bool _isError, string _mes="")
        {
            isError = _isError;
            message = _mes;
        }

        public bool IsError { get => isError; }
        public string Message { get => message;}
    }
}
