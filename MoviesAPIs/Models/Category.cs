
namespace MoviesAPIs.Models
{
	public class Category
	{
		public int Id { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }=string.Empty;
		public List<Movie> Movies { get; set; } = new List<Movie>();	
	}
}
