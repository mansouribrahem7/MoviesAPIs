using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPIs.Dtos
{
	public class MovieDetailsDto
	{
		public string Title { get; set; } = string.Empty;
		public int Year { get; set; }
		[MaxLength(3000)]
		public string StoryLine { get; set; } = string.Empty;
		public double Rate { get; set; }
		public byte[] Poster { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		[MaxLength(100)]
		public string CategoryName { get; set; }=string.Empty;
	}
}
