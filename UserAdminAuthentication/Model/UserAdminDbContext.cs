using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAdminAuthentication.Model
{
    public class UserAdminDbContext : DbContext
    {
        public UserAdminDbContext(DbContextOptions<UserAdminDbContext> options) : base(options)
        {

        }
        public DbSet<UserAdmin> UserAdmins { get; set; }
    }
}
