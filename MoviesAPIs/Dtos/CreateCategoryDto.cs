namespace MoviesAPIs.Dtos
{
	public class CreateCategoryDto
	{
		[MaxLength(100)]
		public string Name { get; set; }=string.Empty;
	}
}
