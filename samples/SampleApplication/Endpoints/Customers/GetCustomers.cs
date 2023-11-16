namespace SampleApplication.Endpoints.Customers
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;

	[PublicAPI]
	public sealed class GetCustomers : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute)
				.Produces<Customer>(200, "application/json");
		}

		public async Task<IResult> Execute(HttpContext httpContext)
		{
			return Results.Ok(new Customer[]
			{
				new Customer
				{
					Name = "John Connor"
				},
				new Customer
				{
					Name = "Sarah Connor"
				}
			});
		}
	}
}