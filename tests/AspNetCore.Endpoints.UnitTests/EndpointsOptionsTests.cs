namespace AspNetCore.Endpoints.UnitTests
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;

	[TestFixture("endpoints")]
	[TestFixture("")]
	[TestFixture(null)]
	public class EndpointsOptionsTests : TestServerFixtureBase
	{
		private readonly string prefix;

		public EndpointsOptionsTests(string prefix)
		{
			this.prefix = prefix;
		}

		/// <inheritdoc />
		protected override Task ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Services.Configure<EndpointsOptions>(options =>
			{
				options.EndpointsRoutePrefix = this.prefix;
			});

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		protected override Task Configure(WebApplication app)
		{
			app.MapEndpoints();

			return Task.CompletedTask;
		}

		[Test]
		public async Task ShouldRegisterEndpoint_GetCustomers()
		{
			HttpClient client = this.CreateClient();

			string path = $"{this.prefix}/customers";

			HttpResponseMessage response = await client.GetAsync(path.TrimStart('/'));
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
		}

		[Test]
		public async Task ShouldRegisterEndpoint_GetCustomer()
		{
			HttpClient client = this.CreateClient();

			string path = $"{this.prefix}/customers/20ac7bae93464ade8803ec34f0cf0b5b";

			HttpResponseMessage response = await client.GetAsync(path.TrimStart('/'));
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
		}
	}
}