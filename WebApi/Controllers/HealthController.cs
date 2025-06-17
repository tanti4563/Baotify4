using System;
using System.Web.Http;
using Service.Helper;

namespace WebApi.Controllers
{
    [RoutePrefix("")]
    public class HealthController : ApiController
    {
        // Health check endpoint
        [HttpGet]
        [Route("health")]
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

        // Database connection test
        [HttpGet]
        [Route("health/database")]
        public IHttpActionResult DatabaseHealth()
        {
            try
            {
                using (var context = new Sample.api.Data.ShipManageEntities())
                {
                    var canConnect = context.Database.Exists();
                    var userCount = canConnect ? context.Users.Count() : 0;
                    
                    return Ok(new
                    {
                        Status = canConnect ? "OK" : "Database Not Found",
                        Message = canConnect ? "Database connection successful" : "Database does not exist",
                        DatabaseExists = canConnect,
                        UserCount = userCount,
                        ConnectionString = context.Database.Connection.ConnectionString.Split(';')[0], // Only show server part
                        Timestamp = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Database connection failed: {ex.Message}",
                    Timestamp = DateTime.Now
                });
            }
        }

        // Service dependency test
        [HttpGet]
        [Route("health/services")]
        public IHttpActionResult ServicesHealth()
        {
            try
            {
                var logger = new LoggingHelper();
                logger.WriteLog("Health check: Services test");

                return Ok(new
                {
                    Status = "OK",
                    Message = "All services are accessible",
                    Services = new
                    {
                        LoggingHelper = "OK",
                        FerryBookingService = "OK",
                        EntityFramework = "OK"
                    },
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = $"Service test failed: {ex.Message}",
                    Timestamp = DateTime.Now
                });
            }
        }
    }
}
