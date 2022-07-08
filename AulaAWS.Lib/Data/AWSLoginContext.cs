using Microsoft.EntityFrameworkCore;
using AulaAWS.Lib.Models;

namespace AulaAWS.Lib.Data
{
    public class AWSLoginContext : DbContext
    {
        public AWSLoginContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("aws_login_usuarios");
            modelBuilder.Entity<Usuario>().HasKey(x => x.Id);
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}