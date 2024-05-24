namespace AspNetCore.Endpoints.UnitTests.Endpoints.Customers
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;

	[PublicAPI]
	[EndpointGroup("Customers")]
	public sealed class GetCustomer : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute, "customers/{id}")
				.Produces<Customer>(200, "application/json");
		}

		public async Task<IResult> Execute(HttpContext httpContext, string id)
		{
			return Results.Ok(new Customer
			{
				Name = "John Connor"
			});
		}
	}
}