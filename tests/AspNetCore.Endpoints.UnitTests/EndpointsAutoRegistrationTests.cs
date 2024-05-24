namespace AspNetCore.Endpoints.UnitTests
{
	using System;
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Builder;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using NUnit.Framework;

	[TestFixture]
	public class EndpointsAutoRegistrationTests : TestServerFixtureBase
	{
		/// <inheritdoc />
		protected override Task ConfigureServices(WebApplicationBuilder builder)
		{
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

			HttpResponseMessage response = await client.GetAsync("api/customers");
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
		}

		[Test]
		public async Task ShouldRegisterEndpoint_GetCustomer()
		{
			HttpClient client = this.CreateClient();

			HttpResponseMessage response = await client.GetAsync("api/customers/20ac7bae93464ade8803ec34f0cf0b5b");
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
		}
	}
}