using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Ferry
{
    public class VNPaymentModel
    {
        public string vnp_Amount { get; set; }
        public string vnp_BankCode { get; set; }
        public string vnp_BankTranNo { get; set; }
        public string vnp_CardType { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_PayDate { get; set; }
        public string vnp_ResponseCode { get; set; }
        public string vnp_TransactionNo { get; set; }
        public string vnp_TxnRef { get; set; }
        public string vnp_SecureHash { get; set; }
    }

    public class SearchVoyageQuery
    {
        public int RouteId { get; set; }
        public string DepartDate { get; set; }
        public int NoOfPassenger { get; set; }
    }

    public class SearchVoyageBackQuery
    {
        public int RouteIdTripGo { get; set; }
        public string DepartDateBack { get; set; }
        public int NoOfPassenger { get; set; }
    }

    public class SeatEmptyQuery
    {
        public int VoyageId { get; set; }
        public string DepartDate { get; set; }
    }

    public class TicketPriceQuery
    {
        public int RouteId { get; set; }
        public int BoatTypeId { get; set; }
        public string DepartDate { get; set; }
    }

    public class BoatLayoutQuery
    {
        public int VoyageId { get; set; }
        public int ScheduleId { get; set; }
    }
}
