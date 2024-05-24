namespace SampleApplication.Endpoints.Customers
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;

	[PublicAPI]
	[EndpointGroup("Customers")]
	public sealed class GetCustomers : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute, "customers")
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