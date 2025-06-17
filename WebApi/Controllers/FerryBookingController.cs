using Service.Helper;
using Service.Implements;
using Service.Interfaces;
using Service.Models;
using Service.Models.Ferry;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("")]
    public class FerryBookingController : ApiController
    {
        private readonly IFerryBookingService _ferryBookingService;
        private readonly LoggingHelper _logger;

        // Parameterless constructor for fallback
        public FerryBookingController()
        {
            _ferryBookingService = new FerryBookingService();
            _logger = new LoggingHelper();
        }

        // Constructor with dependency injection
        public FerryBookingController(IFerryBookingService ferryBookingService)
        {
            _ferryBookingService = ferryBookingService;
            _logger = new LoggingHelper();
        }

        // Step 1: Get Routes
        [HttpGet]
        [Route("Route/GetRoutes")]
        public async Task<IHttpActionResult> GetRoutes()
        {
            try
            {
                _logger.WriteLog("FerryBookingController.GetRoutes called");
                var result = await _ferryBookingService.GetRoutesAsync();
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetRoutes: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 2: Search Voyage
        [HttpGet]
        [Route("OnlineSearchVoyageResult/SearchVoyage")]
        public async Task<IHttpActionResult> SearchVoyage(int RouteId, string DepartDate, int NoOfPassenger)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.SearchVoyage called - RouteId: {RouteId}, DepartDate: {DepartDate}");

                var query = new SearchVoyageQuery
                {
                    RouteId = RouteId,
                    DepartDate = DepartDate,
                    NoOfPassenger = NoOfPassenger
                };

                var result = await _ferryBookingService.SearchVoyageAsync(query);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in SearchVoyage: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 2: Search Voyage Trip Back
        [HttpGet]
        [Route("OnlineSearchVoyageResult/SearchVoyageTripBack")]
        public async Task<IHttpActionResult> SearchVoyageTripBack(int RouteIdTripGo, string DepartDateBack, int NoOfPassenger)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.SearchVoyageTripBack called - RouteId: {RouteIdTripGo}");

                var query = new SearchVoyageBackQuery
                {
                    RouteIdTripGo = RouteIdTripGo,
                    DepartDateBack = DepartDateBack,
                    NoOfPassenger = NoOfPassenger
                };

                var result = await _ferryBookingService.SearchVoyageBackAsync(query);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in SearchVoyageTripBack: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 3: Get Seats Empty
        [HttpGet]
        [Route("Voyage/GetSeatsEmpty")]
        public async Task<IHttpActionResult> GetSeatsEmpty(int voyageId, string departDate)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.GetSeatsEmpty called - VoyageId: {voyageId}");

                var query = new SeatEmptyQuery
                {
                    VoyageId = voyageId,
                    DepartDate = departDate
                };

                var result = await _ferryBookingService.GetSeatsEmptyAsync(query);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetSeatsEmpty: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 3: Get Boat Layout
        [HttpGet]
        [Route("Voyage/GetBoatLayout")]
        public async Task<IHttpActionResult> GetBoatLayout(int voyageId, int scheduleId)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.GetBoatLayout called - VoyageId: {voyageId}");

                var query = new BoatLayoutQuery
                {
                    VoyageId = voyageId,
                    ScheduleId = scheduleId
                };

                var result = await _ferryBookingService.GetBoatLayoutAsync(query);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetBoatLayout: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 3: Get Ticket Price
        [HttpGet]
        [Route("TicketPrice/GetTicketPriceByRouteId")]
        public async Task<IHttpActionResult> GetTicketPriceByRouteId(int RouteId, int BoatTypeId, string DepartDate)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.GetTicketPriceByRouteId called - RouteId: {RouteId}");

                var query = new TicketPriceQuery
                {
                    RouteId = RouteId,
                    BoatTypeId = BoatTypeId,
                    DepartDate = DepartDate
                };

                var result = await _ferryBookingService.GetTicketPriceByRouteIdAsync(query);
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetTicketPriceByRouteId: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 4: Get Nations
        [HttpGet]
        [Route("Nation/GetNations")]
        public async Task<IHttpActionResult> GetNations()
        {
            try
            {
                _logger.WriteLog("FerryBookingController.GetNations called");
                var result = await _ferryBookingService.GetNationsAsync();
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetNations: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 4: Get Ticket Types
        [HttpGet]
        [Route("TicketType/GetTicketTypes")]
        public async Task<IHttpActionResult> GetTicketTypes()
        {
            try
            {
                _logger.WriteLog("FerryBookingController.GetTicketTypes called");
                var result = await _ferryBookingService.GetTicketTypesAsync();
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetTicketTypes: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        // Step 4: Create Order Online
        [HttpPost]
        [Route("OrderOnline/CreateOrderOnline")]
        public IHttpActionResult CreateOrderOnline([FromBody] CreateOrderRequestModel request)
        {
            try
            {
                _logger.WriteLog("FerryBookingController.CreateOrderOnline called");

                if (request == null)
                {
                    return Ok(new ReturnObjectModel
                    {
                        Status = false,
                        Code = "03",
                        Message = "Invalid request data",
                        Data = null
                    });
                }

                var result = _ferryBookingService.CreateOrderOnline(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in CreateOrderOnline: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "03",
                    Message = "Xảy ra lỗi trong quá trình tạo đơn hàng",
                    Data = null
                });
            }
        }

        // Step 5: VNPayment IPN
        [HttpGet]
        [Route("VNPayment/ipnurl")]
        public IHttpActionResult ProcessVNPayment(string vnp_Amount, string vnp_BankCode, string vnp_BankTranNo,
            string vnp_CardType, string vnp_OrderInfo, string vnp_PayDate, string vnp_ResponseCode,
            string vnp_TransactionNo, string vnp_TxnRef, string vnp_SecureHash)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.ProcessVNPayment called for order: {vnp_OrderInfo}");

                var payment = new VNPaymentModel
                {
                    vnp_Amount = vnp_Amount,
                    vnp_BankCode = vnp_BankCode,
                    vnp_BankTranNo = vnp_BankTranNo,
                    vnp_CardType = vnp_CardType,
                    vnp_OrderInfo = vnp_OrderInfo,
                    vnp_PayDate = vnp_PayDate,
                    vnp_ResponseCode = vnp_ResponseCode,
                    vnp_TransactionNo = vnp_TransactionNo,
                    vnp_TxnRef = vnp_TxnRef,
                    vnp_SecureHash = vnp_SecureHash
                };

                var result = _ferryBookingService.ProcessVNPayment(payment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in ProcessVNPayment: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "99",
                    Message = "Các lỗi khác",
                    Data = null
                });
            }
        }

        // Step 5: Get Booking Ticket
        [HttpGet]
        [Route("OrderOnline/GetBookingTicket")]
        public IHttpActionResult GetBookingTicket(string bookingCode)
        {
            try
            {
                _logger.WriteLog($"FerryBookingController.GetBookingTicket called for booking: {bookingCode}");
                var result = _ferryBookingService.GetBookingTicket(bookingCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetBookingTicket: {ex.Message}");
                return Ok(new ReturnObjectModel
                {
                    Status = false,
                    Code = "04",
                    Message = "Không thể tải thông tin đặt vé",
                    Data = null
                });
            }
        }


    }
}