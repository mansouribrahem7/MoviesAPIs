using Microsoft.EntityFrameworkCore;

namespace MoviesAPIs.Models
{
	public class ApplicationContext:DbContext
	{
        public DbSet<Category> Categegories { get; set; }
		public DbSet<Movie> Movies { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
    }

}
