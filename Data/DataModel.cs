//------------------------------------------------------------------------------
// Ferry Booking System - Entity Framework Data Models
// Production-ready entity classes for database operations
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.api.Data
{
    [Table("Users")]
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
            UserRoleAssignments = new HashSet<UserRoleAssignments>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? Gender { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<UserRoleAssignments> UserRoleAssignments { get; set; }
    }

    public partial class UserRoles
    {
        public UserRoles()
        {
            UserRoleAssignments = new HashSet<UserRoleAssignments>();
        }

        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<UserRoleAssignments> UserRoleAssignments { get; set; }
    }

    public partial class UserRoleAssignments
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? AssignedDate { get; set; }

        public virtual Users Users { get; set; }
        public virtual UserRoles UserRoles { get; set; }
    }

    [Table("Orders")]
    public partial class Orders
    {
        public Orders()
        {
            Tickets = new HashSet<Tickets>();
            Payments = new HashSet<Payments>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string BookingCode { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [ForeignKey("Users")]
        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string BookerName { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactEmail { get; set; }

        [StringLength(100)]
        public string BuyerName { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(500)]
        public string CompanyAddress { get; set; }

        [StringLength(50)]
        public string TaxCode { get; set; }

        public int RouteId { get; set; }

        [StringLength(100)]
        public string RouteName { get; set; }

        public int VoyageId { get; set; }
        public int BoatId { get; set; }

        [StringLength(100)]
        public string BoatName { get; set; }

        public int ScheduleId { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }

        [StringLength(100)]
        public string DeparturePort { get; set; }

        [StringLength(100)]
        public string ArrivalPort { get; set; }

        public int TotalPassengers { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? AdditionalFees { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public bool? IsRoundTrip { get; set; }

        [StringLength(20)]
        public string OrderStatus { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual Users Users { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }

    public partial class Tickets
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string TicketNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string PassengerIdNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string PassengerName { get; set; }

        public int PassengerGender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string PlaceOfBirth { get; set; }

        public int NationId { get; set; }

        [StringLength(100)]
        public string NationName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int SeatId { get; set; }

        [Required]
        [StringLength(10)]
        public string SeatNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string SeatClass { get; set; }

        public int PositionId { get; set; }
        public bool? IsVIP { get; set; }
        public int TicketTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string TicketTypeName { get; set; }

        public int TicketPriceId { get; set; }
        public decimal UnitPrice { get; set; }
        public int SequenceNumber { get; set; }

        [StringLength(500)]
        public string QRCode { get; set; }

        [StringLength(20)]
        public string TicketStatus { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Orders Orders { get; set; }
    }

    public partial class Payments
    {
        public Payments()
        {
            PaymentLogs = new HashSet<PaymentLogs>();
        }

        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionId { get; set; }

        [StringLength(100)]
        public string BankTransactionNumber { get; set; }

        public decimal PaymentAmount { get; set; }

        [StringLength(10)]
        public string PaymentCurrency { get; set; }

        [StringLength(50)]
        public string vnp_Amount { get; set; }

        [StringLength(20)]
        public string vnp_BankCode { get; set; }

        [StringLength(100)]
        public string vnp_BankTranNo { get; set; }

        [StringLength(20)]
        public string vnp_CardType { get; set; }

        [StringLength(100)]
        public string vnp_OrderInfo { get; set; }

        [StringLength(20)]
        public string vnp_PayDate { get; set; }

        [StringLength(10)]
        public string vnp_ResponseCode { get; set; }

        [StringLength(100)]
        public string vnp_TransactionNo { get; set; }

        [StringLength(100)]
        public string vnp_TxnRef { get; set; }

        [StringLength(500)]
        public string vnp_SecureHash { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }

        [StringLength(500)]
        public string ResponseMessage { get; set; }

        public string IPNData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual ICollection<PaymentLogs> PaymentLogs { get; set; }
    }

    public partial class PaymentLogs
    {
        public int LogId { get; set; }
        public int PaymentId { get; set; }

        [Required]
        [StringLength(50)]
        public string LogType { get; set; }

        public string LogMessage { get; set; }
        public string LogData { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Payments Payments { get; set; }
    }
}
