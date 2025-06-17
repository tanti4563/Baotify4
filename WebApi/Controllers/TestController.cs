using System;
using System.Collections.Generic;
using System.Web.Http;
using Service.Helper;
using Service.Interfaces;
using Service.Models.Ferry;
using Sample.api.Data;

namespace WebApi.Controllers
{
    [RoutePrefix("")]
    public class TestController : ApiController
    {
        private readonly LoggingHelper _logger;

        public TestController()
        {
            _logger = new LoggingHelper();
        }

        // Simple health check
        [HttpGet]
        [Route("test/health")]
        public IHttpActionResult Health()
        {
            try
            {
                return Ok(new
                {
                    Status = "OK",
                    Message = "Ferry Booking API is running",
                    Timestamp = DateTime.Now,
                    Version = "1.0.0"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = ex.Message,
                    Timestamp = DateTime.Now
                });
            }
        }

        // Test database connection
        [HttpGet]
        [Route("test/database")]
        public IHttpActionResult TestDatabase()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    var canConnect = context.Database.Exists();

                    if (canConnect)
                    {
                        var userCount = context.Users.Count();
                        var orderCount = context.Orders.Count();

                        return Ok(new
                        {
                            Status = "OK",
                            Message = "Database connection successful",
                            DatabaseExists = true,
                            UserCount = userCount,
                            OrderCount = orderCount,
                            ConnectionString = context.Database.Connection.ConnectionString.Split(';')[0],
                            Timestamp = DateTime.Now
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = "Warning",
                            Message = "Database does not exist. Run CreateDatabase.sql script.",
                            DatabaseExists = false,
                            ConnectionString = context.Database.Connection.ConnectionString.Split(';')[0],
                            Timestamp = DateTime.Now
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Database connection failed: {ex.Message}",
                    ErrorDetails = ex.InnerException?.Message,
                    DatabaseExists = false,
                    Timestamp = DateTime.Now
                });
            }
        }

        // Test database connection with simple connection string
        [HttpGet]
        [Route("test/database-simple")]
        public IHttpActionResult TestDatabaseSimple()
        {
            try
            {
                using (var context = ShipManageEntities.CreateWithSimpleConnection())
                {
                    var canConnect = context.Database.Exists();

                    return Ok(new
                    {
                        Status = canConnect ? "OK" : "Warning",
                        Message = canConnect ? "Simple connection successful" : "Database does not exist",
                        DatabaseExists = canConnect,
                        ConnectionType = "Simple SQL Server Connection",
                        Timestamp = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Simple connection failed: {ex.Message}",
                    ErrorDetails = ex.InnerException?.Message,
                    Timestamp = DateTime.Now
                });
            }
        }

        // Test order creation with minimal data
        [HttpPost]
        [Route("test/create-order")]
        public IHttpActionResult TestCreateOrder()
        {
            try
            {
                var testRequest = new CreateOrderRequestModel
                {
                    order = new List<OrderModel>
                    {
                        new OrderModel
                        {
                            Booker = "Test User",
                            ContactNo = "0123456789",
                            Email = "test@example.com",
                            Buyer = "Test User",
                            Taxcode = "",
                            CompNm = "",
                            CompAddress = "",
                            TotalNumber = 1,
                            TotalAmount = 390000,
                            BoatId = 1,
                            VoyageId = 60054,
                            ScheduleId = 84001,
                            PaidAmount = 390000,
                            RouteId = 1,
                            DepartDate = "2025-06-17"
                        }
                    },
                    lstTicketOrders = new List<List<TicketOrderModel>>
                    {
                        new List<TicketOrderModel>
                        {
                            new TicketOrderModel
                            {
                                IdNo = "123456789",
                                FullNm = "Test Passenger",
                                POB = "Ho Chi Minh",
                                YOB = "01/01/1990",
                                TicketTypeId = 1,
                                TicketClass = "LUX",
                                NationId = 1,
                                PhoneNo = "0123456789",
                                Email = "test@example.com",
                                PositionId = 1,
                                TicketPriceId = 1,
                                PriceWithVAT = 390000,
                                No = 1,
                                IsVIP = false,
                                Gender = 0
                            }
                        }
                    },
                    IsRoundTrip = false
                };

                var ferryService = new Service.Implements.FerryBookingService();
                var result = ferryService.CreateOrderOnline(testRequest);

                return Ok(new
                {
                    TestResult = "Order Creation Test",
                    Success = result.Status,
                    Message = result.Message,
                    Data = result.Data,
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TestResult = "Order Creation Test",
                    Success = false,
                    Message = $"Test failed: {ex.Message}",
                    Error = ex.StackTrace,
                    Timestamp = DateTime.Now
                });
            }
        }

        // Test EDMX schema validation
        [HttpGet]
        [Route("test/schema")]
        public IHttpActionResult TestSchema()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // This will trigger schema validation
                    var connectionString = context.Database.Connection.ConnectionString;

                    return Ok(new
                    {
                        Status = "OK",
                        Message = "EDMX schema validation successful",
                        SchemaValid = true,
                        ConnectionType = "Entity Framework with EDMX",
                        Timestamp = DateTime.Now
                    });
                }
            }
            catch (System.Data.Entity.Core.MetadataException ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Schema validation failed: {ex.Message}",
                    SchemaValid = false,
                    ErrorType = "MetadataException",
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Schema test failed: {ex.Message}",
                    SchemaValid = false,
                    ErrorType = ex.GetType().Name,
                    Timestamp = DateTime.Now
                });
            }
        }

        // Test entity creation (no database required)
        [HttpGet]
        [Route("test/entities")]
        public IHttpActionResult TestEntities()
        {
            try
            {
                // Test entity creation
                var user = new Users
                {
                    Username = "testuser",
                    Password = "password123",
                    Email = "test@example.com",
                    FullName = "Test User",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var order = new Orders
                {
                    BookingCode = "12345",
                    OrderNumber = "ORD12345",
                    BookerName = "Test Booker",
                    ContactPhone = "0123456789",
                    ContactEmail = "booker@example.com",
                    RouteId = 1,
                    VoyageId = 1,
                    BoatId = 1,
                    ScheduleId = 1,
                    DepartureDate = DateTime.Now.AddDays(1),
                    DepartureTime = TimeSpan.FromHours(15.75),
                    TotalPassengers = 1,
                    SubTotal = 390000,
                    TotalAmount = 390000,
                    OrderStatus = "Created",
                    PaymentStatus = "Pending",
                    CreatedDate = DateTime.Now
                };

                return Ok(new
                {
                    Status = "OK",
                    Message = "Entity creation test successful",
                    Entities = new
                    {
                        User = new { user.Username, user.Email, user.FullName },
                        Order = new { order.BookingCode, order.OrderNumber, order.TotalAmount }
                    },
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Entity creation test failed: {ex.Message}",
                    Timestamp = DateTime.Now
                });
            }
        }
    }
}
