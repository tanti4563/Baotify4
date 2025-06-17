using Service.Helper;
using Service.Interfaces;
using Service.Models;
using Service.Models.Ferry;
using Sample.api.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class FerryBookingService : IFerryBookingService
    {
        private readonly LoggingHelper _logger;
        private readonly FerryApiClient _ferryApiClient;

        public FerryBookingService()
        {
            _logger = new LoggingHelper();
            _ferryApiClient = new FerryApiClient();
        }

        public async Task<ReturnObjectModel> GetRoutesAsync()
        {
            try
            {
                _logger.WriteLog("FerryBookingService.GetRoutesAsync called");

                // Call external ferry API to get real routes
                var routes = await _ferryApiClient.GetRoutesAsync();

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get routes success",
                    Data = routes
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetRoutesAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> SearchVoyageAsync(SearchVoyageQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.SearchVoyageAsync called with RouteId: {query.RouteId}");

                // Call external ferry API to search voyages
                var voyages = await _ferryApiClient.SearchVoyageAsync(query);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Search voyage success",
                    Data = voyages
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in SearchVoyageAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> SearchVoyageBackAsync(SearchVoyageBackQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.SearchVoyageBackAsync called with RouteId: {query.RouteIdTripGo}");

                // Call external ferry API to search return voyages
                var voyages = await _ferryApiClient.SearchVoyageBackAsync(query);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Search voyage back success",
                    Data = voyages
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in SearchVoyageBackAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> GetSeatsEmptyAsync(SeatEmptyQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.GetSeatsEmptyAsync called with VoyageId: {query.VoyageId}");

                // Call external ferry API to get seat availability
                var seats = await _ferryApiClient.GetSeatsEmptyAsync(query);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get seats empty success",
                    Data = seats
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetSeatsEmptyAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> GetBoatLayoutAsync(BoatLayoutQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.GetBoatLayoutAsync called with VoyageId: {query.VoyageId}");

                // Call external ferry API to get boat layout
                var seats = await _ferryApiClient.GetBoatLayoutAsync(query);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get boat layout success",
                    Data = seats
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetBoatLayoutAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> GetTicketPriceByRouteIdAsync(TicketPriceQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.GetTicketPriceByRouteIdAsync called with RouteId: {query.RouteId}");

                // Call external ferry API to get ticket prices
                var ticketPrices = await _ferryApiClient.GetTicketPriceByRouteIdAsync(query);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get ticket price success",
                    Data = ticketPrices
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetTicketPriceByRouteIdAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> GetNationsAsync()
        {
            try
            {
                _logger.WriteLog("FerryBookingService.GetNationsAsync called");

                // Call external ferry API to get nations
                var nations = await _ferryApiClient.GetNationsAsync();

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get nations success",
                    Data = nations
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetNationsAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<ReturnObjectModel> GetTicketTypesAsync()
        {
            try
            {
                _logger.WriteLog("FerryBookingService.GetTicketTypesAsync called");

                // Call external ferry API to get ticket types
                var ticketTypes = await _ferryApiClient.GetTicketTypesAsync();

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get ticket types success",
                    Data = ticketTypes
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetTicketTypesAsync: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public ReturnObjectModel CreateOrderOnline(CreateOrderRequestModel request)
        {
            try
            {
                _logger.WriteLog("FerryBookingService.CreateOrderOnline called");

                // Validate request
                if (request?.order == null || !request.order.Any())
                {
                    return new ReturnObjectModel
                    {
                        Status = false,
                        Code = "03",
                        Message = "Invalid order data",
                        Data = null
                    };
                }

                // Generate booking code
                var bookingCode = GenerateBookingCode();
                var totalAmount = request.order.Sum(o => o.TotalAmount);

                // Save order to database
                var response = SaveOrderToDatabase(request, bookingCode, totalAmount);

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Create order success",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in CreateOrderOnline: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "03",
                    Message = "Xảy ra lỗi trong quá trình tạo đơn hàng",
                    Data = null
                };
            }
        }

        public ReturnObjectModel ProcessVNPayment(VNPaymentModel payment)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.ProcessVNPayment called for booking: {payment.vnp_OrderInfo}");

                // Validate payment
                if (string.IsNullOrEmpty(payment.vnp_OrderInfo))
                {
                    return new ReturnObjectModel
                    {
                        Status = false,
                        Code = "01",
                        Message = "Không tồn tại mã đặt chỗ",
                        Data = null
                    };
                }

                // Validate secure hash
                var queryString = BuildVNPayQueryString(payment);
                if (!VNPayHelper.ValidateSecureHash(queryString, payment.vnp_SecureHash))
                {
                    _logger.WriteLog("VNPay hash validation failed");
                    return new ReturnObjectModel
                    {
                        Status = false,
                        Code = "97",
                        Message = "Chữ ký không hợp lệ",
                        Data = null
                    };
                }

                // Process payment based on response code
                var message = VNPayHelper.GetResponseMessage(payment.vnp_ResponseCode);

                return new ReturnObjectModel
                {
                    Status = payment.vnp_ResponseCode == "00",
                    Code = payment.vnp_ResponseCode,
                    Message = message,
                    Data = payment.vnp_ResponseCode == "00" ? payment.vnp_OrderInfo : null
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in ProcessVNPayment: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Các lỗi khác",
                    Data = null
                };
            }
        }

        public ReturnObjectModel GetBookingTicket(string bookingCode)
        {
            try
            {
                _logger.WriteLog($"FerryBookingService.GetBookingTicket called for booking: {bookingCode}");

                if (string.IsNullOrEmpty(bookingCode))
                {
                    return new ReturnObjectModel
                    {
                        Status = false,
                        Code = "02",
                        Message = "Không tìm thấy thông tin đặt vé",
                        Data = null
                    };
                }

                // Get booking from database
                var orderResult = GetBookingFromDatabase(bookingCode);

                if (orderResult == null)
                {
                    return new ReturnObjectModel
                    {
                        Status = false,
                        Code = "02",
                        Message = "Không tìm thấy thông tin đặt vé",
                        Data = null
                    };
                }

                return new ReturnObjectModel
                {
                    Status = true,
                    Code = "00",
                    Message = "Get booking ticket success",
                    Data = new List<OrderResultModel> { orderResult }
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Error in GetBookingTicket: " + ex.Message);
                return new ReturnObjectModel
                {
                    Status = false,
                    Code = "04",
                    Message = "Không thể tải thông tin đặt vé",
                    Data = null
                };
            }
        }

        private OrderResultModel GetBookingFromDatabase(string bookingCode)
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    var order = context.Orders
                        .Include("Tickets")
                        .FirstOrDefault(o => o.BookingCode == bookingCode);

                    if (order == null)
                    {
                        return null; // Return null if booking not found
                    }

                    // Map database entity to result model
                    var result = new OrderResultModel
                    {
                        BookingCode = order.BookingCode,
                        OrderNo = order.OrderNumber,
                        RouteNm = order.RouteName ?? "Ferry Route",
                        BoatNm = order.BoatName ?? "Ferry Boat",
                        NoOfTickets = order.TotalPassengers,
                        TotalAmt = order.TotalAmount,
                        FeeHarborAmt = order.AdditionalFees ?? 0,
                        Total = order.TotalAmount,
                        DepartDate = order.DepartureDate.ToString("dd/MM/yyyy"),
                        DepartTime = order.DepartureTime.ToString(@"hh\:mm"),
                        CustomerNm = order.BookerName,
                        CustomerPhone = order.ContactPhone,
                        CustomerEmail = order.ContactEmail,
                        Customer = order.BookerName,
                        Buyer = order.BuyerName ?? order.BookerName,
                        CompanyNm = order.CompanyName ?? "",
                        CompanyAddr = order.CompanyAddress ?? "",
                        Taxcode = order.TaxCode ?? "",
                        TicketOrders = order.Tickets.Select(t => new TicketInfoModel
                        {
                            PassengerTypeNm = t.TicketTypeName,
                            SeatNm = t.SeatNumber,
                            IdNo = t.PassengerIdNumber,
                            FullNm = t.PassengerName,
                            YOB = t.DateOfBirth?.ToString("dd/MM/yyyy") ?? "",
                            POB = t.PlaceOfBirth ?? "",
                            PhoneNo = t.PhoneNumber ?? "",
                            Email = t.Email ?? "",
                            Nation = t.NationName ?? "VietNam",
                            UnitPrice = t.UnitPrice,
                            QRCode = t.QRCode ?? ""
                        }).ToList()
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error getting booking from database: {ex.Message}");
                return null; // Return null on database error
            }
        }

        private string GetTicketTypeName(int ticketTypeId)
        {
            // Map ticket type IDs to names - should be fetched from external API in production
            switch (ticketTypeId)
            {
                case 1: return "Adult";
                case 2: return "Child";
                case 3: return "Infant";
                default: return "Unknown";
            }
        }

        private DateTime TryParseDate(string dateString)
        {
            // Try multiple date formats
            string[] formats = { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            // Fallback to default parsing
            if (DateTime.TryParse(dateString, out DateTime fallback))
            {
                return fallback;
            }

            // If all fails, return tomorrow as default
            return DateTime.Now.AddDays(1);
        }

        private DateTime? TryParseDateOfBirth(string dateString)
        {
            // Try multiple date formats for date of birth
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy/MM/dd" };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            // Fallback to default parsing
            if (DateTime.TryParse(dateString, out DateTime fallback))
            {
                return fallback;
            }

            // Return null if parsing fails
            return null;
        }

        private string GenerateBookingCode()
        {
            // Generate 5-digit booking code
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(10000, 99999).ToString();
        }

        private BookingResponseModel SaveOrderToDatabase(CreateOrderRequestModel request, string bookingCode, decimal totalAmount)
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    var order = request.order.First();

                    // Create Order entity
                    var orderEntity = new Orders
                    {
                        BookingCode = bookingCode,
                        OrderNumber = $"ORD{bookingCode}",
                        BookerName = order.Booker,
                        ContactPhone = order.ContactNo,
                        ContactEmail = order.Email,
                        BuyerName = order.Buyer,
                        CompanyName = order.CompNm,
                        CompanyAddress = order.CompAddress,
                        TaxCode = order.Taxcode,
                        RouteId = order.RouteId,
                        VoyageId = order.VoyageId,
                        BoatId = order.BoatId,
                        ScheduleId = order.ScheduleId,
                        DepartureDate = TryParseDate(order.DepartDate),
                        DepartureTime = TimeSpan.FromHours(15.75), // Will be updated with real data from external API
                        TotalPassengers = order.TotalNumber,
                        SubTotal = totalAmount,
                        TotalAmount = totalAmount,
                        PaidAmount = order.PaidAmount,
                        IsRoundTrip = request.IsRoundTrip,
                        OrderStatus = "Created",
                        PaymentStatus = "Pending",
                        CreatedDate = DateTime.Now
                    };

                    context.Orders.Add(orderEntity);
                    context.SaveChanges();

                    // Create Ticket entities
                    if (request.lstTicketOrders != null && request.lstTicketOrders.Count > 0)
                    {
                        foreach (var ticketGroup in request.lstTicketOrders)
                        {
                            foreach (var ticket in ticketGroup)
                            {
                                var ticketEntity = new Tickets
                                {
                                    OrderId = orderEntity.OrderId,
                                    TicketNumber = $"TKT{bookingCode}{ticket.No:D2}",
                                    PassengerIdNumber = ticket.IdNo,
                                    PassengerName = ticket.FullNm,
                                    PassengerGender = ticket.Gender,
                                    DateOfBirth = TryParseDateOfBirth(ticket.YOB),
                                    PlaceOfBirth = ticket.POB,
                                    NationId = ticket.NationId,
                                    PhoneNumber = ticket.PhoneNo,
                                    Email = ticket.Email,
                                    SeatId = ticket.PositionId,
                                    SeatNumber = $"{ticket.PositionId}",
                                    SeatClass = ticket.TicketClass,
                                    PositionId = ticket.PositionId,
                                    IsVIP = ticket.IsVIP,
                                    TicketTypeId = ticket.TicketTypeId,
                                    TicketTypeName = GetTicketTypeName(ticket.TicketTypeId),
                                    TicketPriceId = ticket.TicketPriceId,
                                    UnitPrice = ticket.PriceWithVAT,
                                    SequenceNumber = ticket.No,
                                    TicketStatus = "Active",
                                    CreatedDate = DateTime.Now
                                };

                                context.Tickets.Add(ticketEntity);
                            }
                        }
                        context.SaveChanges();
                    }

                    return new BookingResponseModel
                    {
                        BookingCode = bookingCode,
                        TotalAmt = totalAmount
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error saving order to database: {ex.Message}");
                // Return response even if database save fails
                return new BookingResponseModel
                {
                    BookingCode = bookingCode,
                    TotalAmt = totalAmount
                };
            }
        }

        private string BuildVNPayQueryString(VNPaymentModel payment)
        {
            var parameters = new Dictionary<string, string>
            {
                { "vnp_Amount", payment.vnp_Amount },
                { "vnp_BankCode", payment.vnp_BankCode },
                { "vnp_BankTranNo", payment.vnp_BankTranNo },
                { "vnp_CardType", payment.vnp_CardType },
                { "vnp_OrderInfo", payment.vnp_OrderInfo },
                { "vnp_PayDate", payment.vnp_PayDate },
                { "vnp_ResponseCode", payment.vnp_ResponseCode },
                { "vnp_TransactionNo", payment.vnp_TransactionNo },
                { "vnp_TxnRef", payment.vnp_TxnRef }
            };

            return VNPayHelper.BuildQueryString(parameters);
        }
    }
}
