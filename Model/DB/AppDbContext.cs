using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace LifetimeToolManage.Model.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tools> Tools { get; set; }
        public DbSet<Lifetime> Lifetimes { get; set; }
        public DbSet<ActiveTool> ActiveTool { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var basePath = AppContext.BaseDirectory;
            var projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            var dbPath = Path.Combine(projectPath, "LTM.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            //optionsBuilder.UseSqlite($"Data Source=LTM.db");
        }
    }
}
