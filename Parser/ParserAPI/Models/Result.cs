using System;
using System.Collections.Generic;
using System.Text;

namespace ParserAPI.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public Object Content { get; set; }
        public Object Detail { get; set; }

        public readonly string Success = "Success";
        
    }
}
