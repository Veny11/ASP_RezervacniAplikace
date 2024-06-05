using Microsoft.EntityFrameworkCore;
using Rezervace_Ples.Models.TableObjects;
namespace Rezervace_Ples.Models.DBConnection
{
    public class MyContext : DbContext
    {
        public DbSet<Lidi> Lide { get; set; }

        public DbSet<Stul> Stoly { get; set; }

        public DbSet<User> Uzivatele { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Rezervace; Integrated Security=True;Trust Server Certificate=true;");
        }
    }
}
