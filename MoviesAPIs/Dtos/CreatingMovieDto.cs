using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPIs.Dtos
{
	public class CreatingMovieDto
	{
		public string Title { get; set; } = string.Empty;
		public int Year { get; set; }
		[MaxLength(3000)]
		public string StoryLine { get; set; }
		public double Rate { get; set; }
		public IFormFile? Poster { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		
	}
}
