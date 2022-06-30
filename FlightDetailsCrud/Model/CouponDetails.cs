using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Model
{
    public class CouponDetails
    {
        [Key]
        public int BookingID { get; set; }
        public string Coupen { get; set; }
        public int discountAmount { get; set; }
    }
}
