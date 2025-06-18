using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Ferry
{
    public class RouteModel
    {
        public int RouteId { get; set; }
        public string Label { get; set; }
        public string Abbrev { get; set; }
        public bool IsHaveAnotherFee { get; set; }
        public List<string> NameFees { get; set; }
        public List<decimal> AmountFees { get; set; }
    }

    public class VoyageModel
    {
        public int BoatTypeId { get; set; }
        public string BoatTypeNm { get; set; }
        public int VoyageId { get; set; }
        public int ScheduleId { get; set; }
        public int RouteId { get; set; }
        public int BoatId { get; set; }
        public string BoatNm { get; set; }
        public string Time { get; set; }
        public int NoOfRemain { get; set; }
        public string DepartTime { get; set; }
        public string Harbor { get; set; }
    }

    public class SeatModel
    {
        public int PositionId { get; set; }
        public int SeatId { get; set; }
        public string TicketClass { get; set; }
        public string SeatNm { get; set; }
        public bool IsSeat { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int IsUpStair { get; set; }
        public bool IsBooked { get; set; }
        public bool IsPublished { get; set; }
        public string BookedInfo { get; set; }
        public bool IsVIP { get; set; }
        public bool IsHeld { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        public bool IsRotate { get; set; }
    }

    public class TicketPriceModel
    {
        public int TicketPriceId { get; set; }
        public int RouteId { get; set; }
        public int TicketTypeId { get; set; }
        public string TicketClass { get; set; }
        public string TicketTypeLabel { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal PriceWithVAT { get; set; }
        public string TmpltNo { get; set; }
        public string Series { get; set; }
    }

    public class TicketTypeModel
    {
        public int TicketTypeId { get; set; }
        public string Label { get; set; }
    }

    public class NationModel
    {
        public int NationId { get; set; }
        public string Label { get; set; }
        public string Abbrev { get; set; }
    }
}
