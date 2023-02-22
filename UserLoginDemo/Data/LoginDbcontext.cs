using Microsoft.EntityFrameworkCore;
using UserLoginDemo.Models;

namespace UserLoginDemo.Data
{
    public class LoginDbcontext : DbContext
    {
        public LoginDbcontext(DbContextOptions<LoginDbcontext> options)
            : base(options)
        {

        }


        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserLog> UserLog { get; set; }
    }
}
