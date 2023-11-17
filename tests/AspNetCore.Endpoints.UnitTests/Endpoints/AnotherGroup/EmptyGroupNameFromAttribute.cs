namespace AspNetCore.Endpoints.UnitTests.Endpoints.AnotherGroup
{
	using AspNetCore.Endpoints.UnitTests.Endpoints.Customers;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;

	[PublicAPI]
	[MadEyeMatt.AspNetCore.Endpoints.EndpointGroupName(null)]
	public sealed class EmptyGroupNameFromAttribute : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute, "{id}")
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