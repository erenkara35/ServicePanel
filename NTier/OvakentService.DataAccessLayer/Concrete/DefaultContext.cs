using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OvakentService.EntityLayer.Concrete;

namespace OvakentService.DataAccessLayer.Concrete
{
    public class DefaultContext : IdentityDbContext<AppUser, AppRole, int>
    {
        //Const geçmeden interfaceler çalışmadı.
        //public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        //{

        //}
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;initial Catalog=TelList;User=erenk;Password=Test12!!;trustServerCertificate=true");
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Arvento> Arventos { get; set; }
    }
}