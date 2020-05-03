using System;
using System.Collections.Generic;
using System.Text;

namespace ParserAPI.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public List<T> Content { get; set; }
        public Object Detail { get; set; }

        public readonly string Success = "Success";
        
    }
}
