using FlightDetailsCrud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFlightCrud.Model
{
    public interface IBookingDetailsRepository
    {
        public List<FlightDetails> SearchFlights(DateTime FlightDateTime, string from, string to, string bookType);

        public TicketBooking BookTicket(TicketBooking bookingInput);
        public List<TicketBooking> SearchBookingWithEmail(string email);

        public TicketBooking SearchBookingWithPnr(string PnrNumber);

        public bool CancelTicket( string PnrNumber);
        public CouponDetails GetCoupen(string Coupen);
    }
}
