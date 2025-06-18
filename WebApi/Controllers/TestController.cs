using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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











    }
}
