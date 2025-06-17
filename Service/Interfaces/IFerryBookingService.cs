using Service.Models;
using Service.Models.Ferry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFerryBookingService
    {
        // Step 1: Get Routes
        Task<ReturnObjectModel> GetRoutesAsync();

        // Step 2: Search Voyages
        Task<ReturnObjectModel> SearchVoyageAsync(SearchVoyageQuery query);
        Task<ReturnObjectModel> SearchVoyageBackAsync(SearchVoyageBackQuery query);

        // Step 3: Get Seats and Prices
        Task<ReturnObjectModel> GetSeatsEmptyAsync(SeatEmptyQuery query);
        Task<ReturnObjectModel> GetBoatLayoutAsync(BoatLayoutQuery query);
        Task<ReturnObjectModel> GetTicketPriceByRouteIdAsync(TicketPriceQuery query);

        // Step 4: Order Creation
        Task<ReturnObjectModel> GetNationsAsync();
        Task<ReturnObjectModel> GetTicketTypesAsync();
        ReturnObjectModel CreateOrderOnline(CreateOrderRequestModel request);

        // Step 5: Payment and Booking
        ReturnObjectModel ProcessVNPayment(VNPaymentModel payment);
        ReturnObjectModel GetBookingTicket(string bookingCode);
    }
}
