using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPIs.Models
{
	public class Movie
	{
		public int Id { get; set; }
		[MaxLength(250)]
		public string Title { get; set; }=string.Empty;
        public int Year { get; set; }
		[MaxLength(3000)]
		public string StoryLine { get; set; }
		public double Rate { get; set; }
        public byte [] Poster { get; set; }
		[ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
