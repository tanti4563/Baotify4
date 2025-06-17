using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class ApiKeyAuthAttribute : AuthorizationFilterAttribute
    {
        private const string API_KEY_HEADER = "HVC.APIKey";
        private const string EXPECTED_API_KEY = "*HVC#*!2e2A";
        private const string BASIC_AUTH_USERNAME = "VNPay.APP";
        private const string BASIC_AUTH_PASSWORD = "ji@u$yzxu@cd92e";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var request = actionContext.Request;
                
                // Check API Key
                if (!request.Headers.Contains(API_KEY_HEADER))
                {
                    actionContext.Response = CreateUnauthorizedResponse("Missing API Key");
                    return;
                }

                var apiKey = request.Headers.GetValues(API_KEY_HEADER).FirstOrDefault();
                if (apiKey != EXPECTED_API_KEY)
                {
                    actionContext.Response = CreateUnauthorizedResponse("Invalid API Key");
                    return;
                }

                // Check Basic Authentication
                if (request.Headers.Authorization == null || 
                    request.Headers.Authorization.Scheme != "Basic")
                {
                    actionContext.Response = CreateUnauthorizedResponse("Missing Basic Authentication");
                    return;
                }

                try
                {
                    var credentials = Encoding.UTF8.GetString(
                        Convert.FromBase64String(request.Headers.Authorization.Parameter));
                    var parts = credentials.Split(':');
                    
                    if (parts.Length != 2 || 
                        parts[0] != BASIC_AUTH_USERNAME || 
                        parts[1] != BASIC_AUTH_PASSWORD)
                    {
                        actionContext.Response = CreateUnauthorizedResponse("Invalid credentials");
                        return;
                    }
                }
                catch
                {
                    actionContext.Response = CreateUnauthorizedResponse("Invalid Basic Authentication format");
                    return;
                }

                // Log successful authentication
                new LoggingHelper().WriteLog($"API authentication successful for {request.RequestUri}");
            }
            catch (Exception ex)
            {
                new LoggingHelper().WriteLog($"Authentication error: {ex.Message}");
                actionContext.Response = CreateUnauthorizedResponse("Authentication error");
            }
        }

        private HttpResponseMessage CreateUnauthorizedResponse(string message)
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent($"{{\"Status\":false,\"Code\":\"401\",\"Message\":\"{message}\",\"Data\":null}}", 
                    Encoding.UTF8, "application/json")
            };
        }
    }
}
