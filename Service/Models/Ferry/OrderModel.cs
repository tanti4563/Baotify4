using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Ferry
{
    public class OrderModel
    {
        public string Booker { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Buyer { get; set; }
        public string Taxcode { get; set; }
        public string CompNm { get; set; }
        public string CompAddress { get; set; }
        public int TotalNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int BoatId { get; set; }
        public int VoyageId { get; set; }
        public int ScheduleId { get; set; }
        public decimal PaidAmount { get; set; }
        public int RouteId { get; set; }
        public string DepartDate { get; set; }
    }

    public class TicketOrderModel
    {
        public string IdNo { get; set; }
        public string FullNm { get; set; }
        public string POB { get; set; }
        public string YOB { get; set; }
        public int TicketTypeId { get; set; }
        public string TicketClass { get; set; }
        public int NationId { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int PositionId { get; set; }
        public int TicketPriceId { get; set; }
        public decimal PriceWithVAT { get; set; }
        public int No { get; set; }
        public bool IsVIP { get; set; }
        public int Gender { get; set; }
    }

    public class CreateOrderRequestModel
    {
        public List<OrderModel> order { get; set; }
        public List<List<TicketOrderModel>> lstTicketOrders { get; set; }
        public bool IsRoundTrip { get; set; }
    }

    // Alias for backward compatibility
    public class CreateOrderOnlineRequestModel : CreateOrderRequestModel
    {
    }

    public class OrderResultModel
    {
        public string BookingCode { get; set; }
        public string OrderNo { get; set; }
        public string RouteNm { get; set; }
        public string BoatNm { get; set; }
        public int NoOfTickets { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal FeeHarborAmt { get; set; }
        public decimal Total { get; set; }
        public string DepartDate { get; set; }
        public string DepartTime { get; set; }
        public string CustomerNm { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Customer { get; set; }
        public string Buyer { get; set; }
        public string CompanyNm { get; set; }
        public string CompanyAddr { get; set; }
        public string Taxcode { get; set; }
        public List<TicketInfoModel> TicketOrders { get; set; }
    }

    public class TicketInfoModel
    {
        public string PassengerTypeNm { get; set; }
        public string SeatNm { get; set; }
        public string IdNo { get; set; }
        public string FullNm { get; set; }
        public string YOB { get; set; }
        public string POB { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Nation { get; set; }
        public decimal UnitPrice { get; set; }
        public string QRCode { get; set; }
    }
}
