using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPIs.Dtos;

namespace MoviesAPIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategegoryController : ControllerBase
	{
		private readonly ApplicationContext _context;
		public CategegoryController(ApplicationContext context)
		{
			_context = context;
		}

		//get all categories
		[HttpGet]
		public async Task<IActionResult> GetAllCategory()
		{
			var AllCat = await _context.Categegories.Include(c=>c.Movies).OrderBy(c => c.Name).ToListAsync();
			return Ok(AllCat);
		}

		//add category
		[HttpPost]
		public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
		{
			Category catToAdd = new Category { Name = dto.Name };
			await _context.AddAsync<Category>(catToAdd);
			_context.SaveChanges();
			return Ok();

		}

		//update category
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id,CreateCategoryDto dto)
		{
			var CatToUpdate=await _context.Categegories.SingleOrDefaultAsync<Category>(c => c.Id == id);
			
			if (CatToUpdate == null)
			{
				return BadRequest("Not Found !!");
			}
			CatToUpdate.Name = dto.Name;
			_context.SaveChanges();
			return Ok(CatToUpdate);

		}
		//delete category
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var catToDelete= await _context.Categegories.SingleOrDefaultAsync<Category>(c=>c.Id == id);
			if (catToDelete == null)
			{
				return NotFound($"No Category wit id {id}");
			}
			_context.Remove(catToDelete);
			_context.SaveChanges();
			return Ok("Deleted Succcessfully !");
		}


	}
}
