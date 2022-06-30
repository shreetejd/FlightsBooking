using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFlightCrud.Model
{
    public class BookingHistory
    {
        public int BookingID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int TotalSeat { get; set; }

        public string PassingerDetails { get; set; }
        public string MealType { get; set; }
        public string SeatNumbers { get; set; }
        public string PnrNumber { get; set; }
        public string BookingStatus { get; set; }

        public string BookType { get; set; }
        public DateTime BookingTime { get; set; }

        public int FlightId { get; set; }

        public string Airlinename { get; set; }
    }
}
