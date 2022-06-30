using BookFlightCrud.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFlightCrud.Controllers
{
    public class BookingController : ControllerBase
    {
        private readonly IBookingDetailsRepository bookingDetails;
        //private readonly UserAdmin  input;
        public BookingController(IBookingDetailsRepository _bookingDetails)
        {
            bookingDetails = _bookingDetails;
        }


        #region search
        [Route("api/booking/SearchFlight")]
        [HttpGet("SearchFlight")]
        public async Task<ActionResult<String>> Search(DateTime FlightDateTime, string from, string to, string bookType)
        {
            var retVal = bookingDetails.SearchFlights(FlightDateTime, from, to, bookType);
            return retVal != null ? Ok(System.Text.Json.JsonSerializer.Serialize(retVal)) : "No flights found for above Search";
        }
        #endregion

        #region booking
        [Route("api/booking/BookTicket")]
        [HttpPost("BookTicket")]
        public async Task<ActionResult<String>> BookTicket(TicketBooking bookingInput)
        {
            var retVal = bookingDetails.BookTicket(bookingInput);
            return retVal != null ? Ok("Ticket Booked Succesfully.!!\nPnr Number : " + retVal.PnrNumber) : "Please try later";
        }
        #endregion

        #region Search Booking history
        [Route("api/booking/SearchBooking")]
        [HttpGet("SearchBooking")]
        public async Task<ActionResult<String>> SearchBooking(string email)
        {
           var details = bookingDetails.SearchBookingWithEmail(email);

            return details != null ? Ok(System.Text.Json.JsonSerializer.Serialize(details)):"please enter valid email";
        }

        [Route("api/booking/SearchPnr")]
        [HttpGet("SearchPnr")]
        public async Task<ActionResult<String>> SearchPnr(string PnrNumber)
        {
            var details = bookingDetails.SearchBookingWithPnr(PnrNumber);

            return details != null ? Ok(System.Text.Json.JsonSerializer.Serialize(details)) : "please enter valid Pnr Number";
        }

        #endregion


        #region cancel Booking
        [Route("api/booking/CancelTicket")]
        [HttpPost("CancelTicket")]
        public async Task<ActionResult<String>> CancelTicket(string PnrNumber)
        {

            return bookingDetails.CancelTicket( PnrNumber) ? Ok("ticket cancelled..!!") : "please enter valid email/Pnr Number";
        }

        #endregion
        [Route("api/booking/GetCoupen")]
        [HttpGet("GetCoupen")]
        public async Task<ActionResult<String>> GetCoupen(string Coupen)
        {
            var details = bookingDetails.GetCoupen(Coupen);

            return details != null ? Ok(System.Text.Json.JsonSerializer.Serialize(details)) : "please enter valid coupen";
        }
    }
}
