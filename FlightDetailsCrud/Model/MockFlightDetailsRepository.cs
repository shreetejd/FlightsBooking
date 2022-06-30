using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Model
{
    public class MockFlightDetailsRepository : IFlightDetailsRepository
    {
        private readonly FlightDetailsDbContext context;
        public MockFlightDetailsRepository(FlightDetailsDbContext Context)
        {
            this.context = Context;
        }

        public bool AddCoupen(CouponDetails input)
        {
            var coupon = context.CouponDetails.Where(s => s.Coupen == input.Coupen).FirstOrDefault();
            if (coupon != null)
            {

                return false;
            }
            context.CouponDetails.Add(input);
            context.SaveChanges();
            return true;
        }

        public bool AddFlight(FlightDetails input)
        {
            var flight = context.flightDetails.Where(s => s.FlightNumber == input.FlightNumber).FirstOrDefault();
            if (flight != null)
            {

               return false;
            }
            context.flightDetails.Add(input);
            context.SaveChanges();
            return true;
        }

        public bool DeleteFlight(int FlightNumber)
        {
           var flight = context.flightDetails.Where(s => s.FlightNumber == FlightNumber).FirstOrDefault();
            if (flight != null)
            {
                context.flightDetails.Remove(flight);
                context.SaveChanges();
                return true;
            }
            return false;

        }

        public List<FlightDetails> GetFlightDetails()
        {
           var details = context.flightDetails.ToList();
            return details;
        }

        public FlightDetails GetFlightDetailsbyId(int Id)
        {
            return context.flightDetails.Where(s => s.FlightNumber == Id).FirstOrDefault();
            
        }

        public bool UpdateFlight(FlightDetails input)
        {
            var flight = context.flightDetails.Where(s => s.FlightNumber == input.FlightNumber).FirstOrDefault();

            if (flight != null)
            {
                flight.Airline = input.Airline;
                flight.BusinessSeatCount = input.BusinessSeatCount.HasValue ? input.BusinessSeatCount : flight.BusinessSeatCount;
                flight.Cost = input.Cost.HasValue ? input.Cost : flight.Cost;
                flight.Days = !String.IsNullOrEmpty(input.Days) ? input.Days : flight.Days;
                flight.FromPlace = !String.IsNullOrEmpty(input.FromPlace) ? input.FromPlace : flight.FromPlace;
                flight.ToPlace = !String.IsNullOrEmpty(input.ToPlace) ? input.ToPlace : flight.ToPlace;
                flight.IsBlocked = !String.IsNullOrEmpty(input.IsBlocked.ToString()) ? input.IsBlocked : flight.IsBlocked;
                flight.Meal = !string.IsNullOrEmpty(input.Meal) ? input.Meal : flight.Meal;



                context.flightDetails.Update(flight);
                context.SaveChanges();
                return true;
            }
           
                return false;

        }
    }
}
