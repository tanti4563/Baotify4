using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ReturnObjectModel
    {
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class BookingResponseModel
    {
        public string BookingCode { get; set; }
        public decimal TotalAmt { get; set; }
    }
}
