using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAdminAuthentication.Model
{
    public interface IUserAdminRepository
    {
        //public UserAdmin Register(UserAdmin input);
        public bool Register(UserAdmin userAdmin, string password);
        public UserAdmin login(string email, string password );

        public string CreateToken(UserAdmin userAdmin);
        public RefreshToken GenerateRefreshToken();
    }
}
