namespace SampleApplication.Endpoints.WeatherForecast
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;

	[PublicAPI]
	[EndpointName("Weather")]
	public sealed class GetWeatherForecasts : EndpointBase
	{
		private string[] summaries = new string[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapGet(this.Execute, "weather");
		}

		public async Task<IEnumerable<WeatherForecast>> Execute(HttpContext httpContext)
		{
			WeatherForecast[] forecast = Enumerable.Range(1, 5).Select(index =>
					new WeatherForecast
					{
						Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
						TemperatureC = Random.Shared.Next(-20, 55),
						Summary = this.summaries[Random.Shared.Next(this.summaries.Length)]
					})
				.ToArray();

			return forecast;
		}
	}
}