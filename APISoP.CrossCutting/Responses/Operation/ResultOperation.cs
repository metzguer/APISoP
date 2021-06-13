using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Responses.Operation
{
    public class ResultOperation
    {
        public bool Success { get; set; }
        public ICollection<ItemError> Errors { get; set; }
        public string Message { get; set; }
        public ResultOperation()
        {
            Success = false;
            Errors = new List<ItemError>();
        }
    }

    public class ResultOperation<T>
    {
        public bool Success { get; set; }
        public ICollection<ItemError> Errors { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }

        public ResultOperation()
        {
            Result = default(T);
            Success = false;
            Errors = new List<ItemError>();
        }
    }
}
