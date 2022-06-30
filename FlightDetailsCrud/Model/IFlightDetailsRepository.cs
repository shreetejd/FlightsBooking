using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Model
{
    public interface IFlightDetailsRepository
    {
        public bool AddFlight(FlightDetails input);
        public bool UpdateFlight(FlightDetails input);
        public bool DeleteFlight(int FlightNumber);

        public List<FlightDetails> GetFlightDetails();
        public FlightDetails GetFlightDetailsbyId(int ID);
        public bool AddCoupen(CouponDetails input);
    }
}
