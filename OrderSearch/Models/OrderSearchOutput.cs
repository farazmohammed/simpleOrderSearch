using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderSearch.Models
{
    public class OrderSearchOutput
    {
        /// <summary>
        /// Possible values 0 - No error | 1 - Warning | 2 - error 
        /// </summary>
        public int ServiceError { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
        public string OrderData { get; set; }

    }
}