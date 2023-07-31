
using Microsoft.EntityFrameworkCore;
using MoviesAPIs.Models;

namespace MoviesAPIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var Myconnection = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationContext>(
				options=>options.UseSqlServer(Myconnection)
				);

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCors();	

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}