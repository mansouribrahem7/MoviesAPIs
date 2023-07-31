using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPIs.Dtos;

namespace MoviesAPIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		readonly ApplicationContext _context;
		readonly List<string> _allowedFormats = new() { ".jpg", ".png" };
		readonly long _maxSize = 1048576;

		public MoviesController(ApplicationContext context)
		{
			_context = context;
		}

		///////////////////getting all movies
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var Movies = await _context.Movies
				.Include(m => m.Category)
				.ToListAsync();
			return Ok(Movies);
		}



		///////////////////////get by id 

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			//get the movie with eager loading (include)
			var Movie = await _context.Movies.Include(c=>c.Category).SingleOrDefaultAsync(m => m.Id == id);
			if (Movie == null)
				return NotFound("No Movie with this id!");
			MovieDetailsDto dto = new MovieDetailsDto
			{
				CategoryId = Movie.CategoryId,
				Title = Movie.Title,
				CategoryName = Movie.Category.Name,
				Rate = Movie.Rate,
				Poster = Movie.Poster,
				StoryLine = Movie.StoryLine,
				Year = Movie.Year,
			};
			return Ok(dto);

		}


		////////////////get Movies by categeogry id 
		[HttpGet("//{CatId}")]
		public async Task<IActionResult> GetByCategory(int CatId)
		{
			var Movies = await _context.Movies.Include(m=>m.Category).Where(c => c.CategoryId == CatId).ToListAsync();
			if (Movies == null)
				return NotFound("No Movies in this category!");
			return Ok(Movies);
		}




		/////////////////////////////adding
		[HttpPost]
		public async Task<IActionResult> AddMovie([FromForm]CreatingMovieDto dto)
		{
			//checking format
			if (!_allowedFormats.Contains(Path.GetExtension( dto.Poster.FileName.ToLower())))
				return BadRequest("Only png and jpg are allowed formats");


			//checking size
			if (dto.Poster.Length > _maxSize)
				return BadRequest("The max size is 1MB !");

			//checking forgin key 
			var _isFound= await _context.Categegories.AnyAsync(c=>c.Id==dto.CategoryId);
			if (!_isFound)
				return BadRequest("Invaild Category");


			//checking nullable

			if (dto.Poster == null) return BadRequest("Poster is Required");

			using var streamdata = new MemoryStream();
			await dto.Poster.CopyToAsync(streamdata);
			Movie MovieToAdd = new Movie
			{
				Title = dto.Title,
				CategoryId = dto.CategoryId,

				Rate = dto.Rate,
				Year = dto.Year,
				StoryLine = dto.StoryLine,
				Poster = streamdata.ToArray(),

			};

			await _context.Movies.AddAsync(MovieToAdd);
			_context.SaveChanges();
			return Ok(MovieToAdd);
		}

		///////////////////deleting
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteById(int id)
		{
			var Movie = await _context.Movies.SingleOrDefaultAsync(c => c.Id==id);
			if (Movie == null)
				return NotFound("No movies with this id!");
			_context.Movies.Remove(Movie);
			_context.SaveChanges();
			return Ok("Deleted Sussessfully!");
		}
		//updating 
		[HttpPut]
		public async Task<IActionResult> Update (int id, [FromForm]CreatingMovieDto dto)
		
		{
			var DbMovie = await _context.Movies.SingleOrDefaultAsync(db => db.Id == id);
			if (DbMovie == null)
				return NotFound("No Movie With this id !");

			//checking forgin key 
			var _isFound = await _context.Categegories.AnyAsync(c => c.Id == dto.CategoryId);
			if (!_isFound)
				return BadRequest("Invaild Category");

			if (dto.Poster!=null)
			{
				if (!_allowedFormats.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
					return BadRequest("This format is not allowed !");
				if (dto.Poster.Length > _maxSize)
					return BadRequest("Max Size is 1MB !");
				using var sreamdata = new MemoryStream();
				dto.Poster.CopyTo(sreamdata);
				DbMovie.Poster = sreamdata.ToArray();
			}
			DbMovie.Title= dto.Title;
			DbMovie.StoryLine= dto.StoryLine;
			DbMovie.Rate = dto.Rate;
			DbMovie.Year= dto.Year;
			DbMovie.CategoryId= dto.CategoryId;

			_context.SaveChanges();
			return Ok(DbMovie);



		}

	}
}
