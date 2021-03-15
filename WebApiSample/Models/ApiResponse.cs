using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSample.Models
{
    public class ApiResponse<T>
    {
        public int resultCount { get; set; }
        public T result { get; set; }
        public string message { get; set; }
    }
}
