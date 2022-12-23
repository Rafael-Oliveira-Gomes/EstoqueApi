using EstoqueApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Context {
    public class MySqlContext : IdentityDbContext<ApplicationUser> {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            modelBuilder.Entity<Produto>();
        }
    }
}
