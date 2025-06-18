using Service.Models.Ferry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Service.Helper;

namespace Service.Helper
{
    public class FerryApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly LoggingHelper _logger;
        private const string BASE_URL = "http://192.168.1.125:7878";
        private const string USERNAME = "VNPay.APP";
        private const string PASSWORD = "ji@u$yzxu@cd92e";
        private const string API_KEY = "*HVC#*!2e2A";

        public FerryApiClient()
        {
            _httpClient = new HttpClient();
            _logger = new LoggingHelper();
            SetupHttpClient();
        }

        private void SetupHttpClient()
        {
            // Set base address
            _httpClient.BaseAddress = new Uri(BASE_URL);

            // Set authentication headers
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{USERNAME}:{PASSWORD}"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

            // Set API key header
            _httpClient.DefaultRequestHeaders.Add("HVC.APIKey", API_KEY);

            // Set accept header (Content-Type will be set per request)
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<RouteModel>> GetRoutesAsync()
        {
            try
            {
                _logger.WriteLog("FerryApiClient.GetRoutesAsync called");

                var response = await _httpClient.GetAsync("/Route/GetRoutes");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var routes = JsonConvert.DeserializeObject<List<RouteModel>>(content);
                    _logger.WriteLog($"Retrieved {routes?.Count ?? 0} routes from external API");
                    return routes ?? new List<RouteModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<RouteModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API GetRoutes: {ex.Message}");
                return new List<RouteModel>();
            }
        }

        private List<RouteModel> GetFallbackRouteData()
        {
            _logger.WriteLog("Returning fallback route data for testing");

            return new List<RouteModel>
            {
               
            };
        }

        public async Task<List<VoyageModel>> SearchVoyageAsync(SearchVoyageQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryApiClient.SearchVoyageAsync called with RouteId: {query.RouteId}");

                var url = $"/OnlineSearchVoyageResult/SearchVoyage?RouteId={query.RouteId}&DepartDate={query.DepartDate}&NoOfPassenger={query.NoOfPassenger}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var voyages = JsonConvert.DeserializeObject<List<VoyageModel>>(content);
                    _logger.WriteLog($"Retrieved {voyages?.Count ?? 0} voyages from external API");
                    return voyages ?? new List<VoyageModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<VoyageModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API SearchVoyage: {ex.Message}");
                return new List<VoyageModel>();
            }
        }



        public async Task<List<VoyageModel>> SearchVoyageBackAsync(SearchVoyageBackQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryApiClient.SearchVoyageBackAsync called with RouteId: {query.RouteIdTripGo}");

                var url = $"/OnlineSearchVoyageResult/SearchVoyageTripBack?RouteIdTripGo={query.RouteIdTripGo}&DepartDateBack={query.DepartDateBack}&NoOfPassenger={query.NoOfPassenger}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var voyages = JsonConvert.DeserializeObject<List<VoyageModel>>(content);
                    return voyages ?? new List<VoyageModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<VoyageModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API SearchVoyageBack: {ex.Message}");
                return new List<VoyageModel>();
            }
        }

        public async Task<List<SeatModel>> GetSeatsEmptyAsync(SeatEmptyQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryApiClient.GetSeatsEmptyAsync called with VoyageId: {query.VoyageId}");

                var url = $"/Voyage/GetSeatsEmpty?voyageId={query.VoyageId}&departDate={query.DepartDate}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var seats = JsonConvert.DeserializeObject<List<SeatModel>>(content);
                    _logger.WriteLog($"Retrieved {seats?.Count ?? 0} empty seats from API");
                    return seats ?? new List<SeatModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<SeatModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetSeatsEmptyAsync: {ex.Message}");
                return new List<SeatModel>();
            }
        }

        public async Task<List<SeatModel>> GetBoatLayoutAsync(BoatLayoutQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryApiClient.GetBoatLayoutAsync called with VoyageId: {query.VoyageId}");

                var url = $"/Voyage/GetBoatLayout?voyageId={query.VoyageId}&scheduleId={query.ScheduleId}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var seats = JsonConvert.DeserializeObject<List<SeatModel>>(content);
                    _logger.WriteLog($"Retrieved {seats?.Count ?? 0} seats from boat layout API");
                    return seats ?? new List<SeatModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<SeatModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error in GetBoatLayoutAsync: {ex.Message}");
                return new List<SeatModel>();
            }
        }

        public async Task<List<TicketPriceModel>> GetTicketPriceByRouteIdAsync(TicketPriceQuery query)
        {
            try
            {
                _logger.WriteLog($"FerryApiClient.GetTicketPriceByRouteIdAsync called with RouteId: {query.RouteId}");

                var url = $"/TicketPrice/GetTicketPriceByRouteId?RouteId={query.RouteId}&BoatTypeId={query.BoatTypeId}&DepartDate={query.DepartDate}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var prices = JsonConvert.DeserializeObject<List<TicketPriceModel>>(content);
                    return prices ?? new List<TicketPriceModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<TicketPriceModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API GetTicketPriceByRouteId: {ex.Message}");
                return new List<TicketPriceModel>();
            }
        }

        public async Task<List<NationModel>> GetNationsAsync()
        {
            try
            {
                _logger.WriteLog("FerryApiClient.GetNationsAsync called");

                var response = await _httpClient.GetAsync("/Nation/GetNations");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var nations = JsonConvert.DeserializeObject<List<NationModel>>(content);
                    return nations ?? new List<NationModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<NationModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API GetNations: {ex.Message}");
                return new List<NationModel>();
            }
        }

        public async Task<List<TicketTypeModel>> GetTicketTypesAsync()
        {
            try
            {
                _logger.WriteLog("FerryApiClient.GetTicketTypesAsync called");

                var response = await _httpClient.GetAsync("/TicketType/GetTicketTypes");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ticketTypes = JsonConvert.DeserializeObject<List<TicketTypeModel>>(content);
                    return ticketTypes ?? new List<TicketTypeModel>();
                }
                else
                {
                    _logger.WriteLog($"Ferry API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<TicketTypeModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error calling Ferry API GetTicketTypes: {ex.Message}");
                return new List<TicketTypeModel>();
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
