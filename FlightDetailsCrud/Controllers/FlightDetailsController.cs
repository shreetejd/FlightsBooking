using FlightDetailsCrud.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Controllers
{
    public class FlightDetailsController : ControllerBase
    {
        private readonly IFlightDetailsRepository flightDetails;
        //private readonly UserAdmin  input;
        public FlightDetailsController(IFlightDetailsRepository _flightDetails)
        {
            flightDetails = _flightDetails;
        }

        
        #region Add Flight Details
        [Route("api/auth/AddFlight")]
        [HttpPost("AddFlight")]
        public async Task<ActionResult<String>> AddFlight([FromQuery]FlightDetails details)
        {
             return flightDetails.AddFlight(details) ?  Ok("Flight Added"):Ok("Not Added, Please try later");
            
        }

        #endregion

        #region Update Flight Details
        [Route("api/auth/UpdateFlight")]
        [HttpPost("UpdateFlight")]
        public async Task<ActionResult<String>> UpdateFlight([FromQuery] FlightDetails details)
        {
            return flightDetails.UpdateFlight(details) ? Ok("Flight Details Updated") : Ok("Flight Details Not Updated, Please enter valid flight Number");

        }

        #endregion

        #region Delete Flight details
       
        [Route("api/auth/DeleteFlight")]
        [HttpPost("DeleteFlight")]
        public async Task<ActionResult<String>> DeleteFlight(int FlightNumber)
        {
            return flightDetails.DeleteFlight(FlightNumber) ? Ok("Flight Deleted") : Ok("Not Deleted, Please try later");

        }
        #endregion

        #region Get flights
        [Route("api/auth/GetFlight")]
        [HttpGet("GetFlight")]
        public async Task<ActionResult<String>> GetFlight()
        {
            var flightdetails = flightDetails.GetFlightDetails();
            return  flightDetails!=null ? Ok(System.Text.Json.JsonSerializer.Serialize(flightdetails)) : Ok("No flights found");

        }
        [Route("api/auth/GetFlightbyId")]
        [HttpGet("GetFlightbyId")]
        public async Task<ActionResult<String>> GetFlightbyId(int FlightNumber)
        {
            var flightdetails = flightDetails.GetFlightDetailsbyId(FlightNumber);
            return flightDetails != null ? Ok(System.Text.Json.JsonSerializer.Serialize(flightdetails)) : Ok("No flights found");

        }
        #endregion

        #region Add Coupen Details
        [Route("api/auth/AddCoupen")]
        [HttpPost("AddCoupen")]
        public async Task<ActionResult<String>> AddCoupen([FromQuery] CouponDetails details)
        {
            return flightDetails.AddCoupen(details) ? Ok("Coupen Added") : Ok("Coupen Alredy exist, Please try later");

        }

        #endregion

    }
}
