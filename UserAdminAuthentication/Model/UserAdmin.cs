using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAdminAuthentication.Model
{
    public class UserAdmin
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] HashPassword { get; set; }
        
        public byte[] SaltPassword { get; set; }
        public bool IsAdmin { get; set; }
        public string Gender { get; set; }
        
        public int? PhoneNumber { get; set; }




    }
}
