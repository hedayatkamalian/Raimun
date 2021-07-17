using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Raimun.App.Entities.Data
{
	public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> {

		public ApplicationDbContext CreateDbContext(string[] args) {


			var configuration = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("appsettings.Development.json")
				.Build();


			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));


			return new ApplicationDbContext(optionsBuilder.Options);
		}

	}
}
