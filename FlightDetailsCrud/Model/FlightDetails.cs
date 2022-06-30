using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Model
{
    public class FlightDetails
    {
        [Key]
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public string Airline{ get; set; }
        public string FromPlace { get; set; }

        public string ToPlace { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Days { get; set; }
        public string Instruments { get; set; }
        public int? BusinessSeatCount{ get; set; }
        public int? NonBusinessSeatCount { get; set; }
        public double? Cost { get; set; }

        public bool IsBlocked { get; set; }
        public string Meal { get; set; }
        public int? NumOfRows { get; set; }
    }
}
