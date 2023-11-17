namespace SampleApplication.Endpoints.WeatherForecast
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
