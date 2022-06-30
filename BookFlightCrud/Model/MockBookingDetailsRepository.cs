using FlightDetailsCrud.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BookFlightCrud.Model
{
    public class MockBookingDetailsRepository : IBookingDetailsRepository
    {
        private readonly BookingDbContext context;
        private readonly FlightDetailsDbContext flightContext;
        public MockBookingDetailsRepository(BookingDbContext Context, FlightDetailsDbContext _flightcontext)
        {
            this.context = Context;
            this.flightContext = _flightcontext;
        }

        public TicketBooking BookTicket([FromBody] TicketBooking bookingInput)
        {

            try
            {
                var flight = context.Add(bookingInput);
                flight.Entity.BookingStatus = "Booked";
                if (context.TicketBookings.Any())
                {
                    var lastPnrvalue = context.TicketBookings.OrderByDescending(s => s.BookingID).Take(1);
                    var lastPnr = lastPnrvalue.Select(s => s.PnrNumber).FirstOrDefault();
                    int value = Convert.ToInt32(lastPnr.Trim().Substring(3)) + 1;
                    flight.Entity.PnrNumber = "PNR" + value.ToString();
                    context.SaveChanges();

                    return flight.Entity;
                }
                else
                {
                    flight.Entity.PnrNumber = "PNR" + 1000;
                    context.SaveChanges();
                    return flight.Entity;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CancelTicket(string PnrNumber)
        {

            if (!string.IsNullOrEmpty(PnrNumber))
            {
                var ticket = context.TicketBookings.Where(s =>  s.PnrNumber == PnrNumber).FirstOrDefault();

                if (ticket != null)
                {
                    ticket.BookingStatus = "cancelled";
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public CouponDetails GetCoupen(string Coupen)
        {
            var coupenDetails = this.flightContext.CouponDetails.Where(s => s.Coupen == Coupen).FirstOrDefault();
            return coupenDetails;
        }

        public List<TicketBooking> SearchBookingWithEmail(string email)
        {
            var details = context.TicketBookings.Where(s => s.Email == email).ToList();
            //var json = System.Text.Json.JsonSerializer.Serialize(details);
            if (details != null)
            {
                return details;
            }
            return null;
        }

        public TicketBooking SearchBookingWithPnr(string PnrNumber)
        {
            var details = context.TicketBookings.Where(s => s.PnrNumber == PnrNumber).FirstOrDefault();
            //var Airlinename = flightContext.flightDetails.Where(s => s.FlightNumber == details.FlightId).Select(s => s.Airline);
            //var json = System.Text.Json.JsonSerializer.Serialize(details);
            //var history = new BookingHistory()
            //{
            //    Airlinename = "",
            //    //Airlinename = Airlinename.ToString(),
            //    BookingID = details.BookingID,
            //    BookingTime = details.BookingTime,
            //    BookingStatus = details.BookingStatus,
            //    BookType = details.BookType,
            //    Email = details.Email,
            //    FlightId = details.FlightId,
            //    MealType = details.MealType,
            //    Name = details.MealType,
            //    PnrNumber = details.PnrNumber,
            //    PassingerDetails = details.PassingerDetails,
            //    SeatNumbers = details.PassingerDetails,
            //    TotalSeat = details.TotalSeat,


            //};

            if (details != null)
            {

                return details;
            }
            return null;
        }

        public List<FlightDetails> SearchFlights([FromBody] DateTime FlightDateTime, string from, string to, string bookType)
        {
            var Flights = flightContext.flightDetails.Where(s => s.StartTime.Date == FlightDateTime.Date && s.FromPlace == from && s.ToPlace == to && (s.IsBlocked == false)).ToList();
            if (Flights != null)
            {
                return Flights;
            }
            return null;
        }
    }
}
