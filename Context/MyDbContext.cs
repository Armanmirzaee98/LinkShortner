using Microsoft.EntityFrameworkCore;
using My_LinkShortener_App.Models;

namespace My_LinkShortener_App.Context
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext>options):base(options)
        {
        }
        public DbSet<Links> Links { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}