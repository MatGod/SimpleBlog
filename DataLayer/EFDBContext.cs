using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer {
	public class EFDBContext : DbContext {
		public DbSet<Directory> Directory { get; set; }
		public DbSet<Material> Material { get; set; }
		
		public EFDBContext(DbContextOptions<EFDBContext> options):base(options) {}
	}

	public class EDFBContextFactory : IDesignTimeDbContextFactory<EFDBContext> {
		public EFDBContext CreateDbContext(string[] args) {
			var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
			optionsBuilder
				.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataBase;Trusted_Connection=True;MultipleActiveResultSets=true",
				              b => b.MigrationsAssembly("DataLayer"));
			optionsBuilder.EnableSensitiveDataLogging();
			return new EFDBContext(optionsBuilder.Options);
		}
	}
}