namespace SampleApplication
{
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.Extensions.DependencyInjection;

	public static class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			// Add the endpoints as services.
			builder.Services.AddEndpoints();

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.Configure<EndpointsOptions>(options =>
			{
				options.EndpointsRoutePrefix = "endpoints";
				options.MapGroup = groupBuilder =>
				{
#if NET8_0_OR_GREATER
					groupBuilder.WithOpenApi();
#endif
				};
			});

			WebApplication app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapEndpoints();

			app.Run();
		}
	}
}
