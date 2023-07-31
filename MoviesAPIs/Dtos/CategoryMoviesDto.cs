namespace MoviesAPIs.Dtos
{
	public class CategoryMoviesDto
	{
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;
		public List<Movie> Movies { get; set; } = new List<Movie>();
	}
}
