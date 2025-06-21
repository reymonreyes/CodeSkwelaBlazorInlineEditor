using Microsoft.EntityFrameworkCore;
using TableInlineEditor.Services.Models;

namespace TableInlineEditor.Services
{
    public class ApplicationDbContext : DbContext
    {
        public string DbPath;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(folder, "Services\\northwind.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }        
    }
}
