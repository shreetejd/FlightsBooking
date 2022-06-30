using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDetailsCrud.Model
{
    public class FlightDetailsDbContext : DbContext
    {
        public FlightDetailsDbContext(DbContextOptions<FlightDetailsDbContext> options) : base(options)
        {

        }
        public DbSet<FlightDetails> flightDetails { get; set; }
        public DbSet<CouponDetails> CouponDetails { get; set; }
        
    }
}
