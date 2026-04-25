using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Normalda DbContext yazılır, amma Identity üçün IdentityDbContext-dən miras alırıq
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Dapperlə işlədiyimiz üçün digər entity-ləri (Category və s.) 
        // bura DbSet olaraq əlavə etməsən də olar. 
        // Bura hələlik ancaq Identity cədvəlləri üçün cavabdehdir.
    }
}
