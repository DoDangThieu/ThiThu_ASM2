using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class MyDbcontext : DbContext
    {
        public MyDbcontext()
        {
            
        }

        public MyDbcontext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS;Database=Test12;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
    }
}
