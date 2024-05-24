namespace SampleApplication.Endpoints
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;

	[PublicAPI]
	[EndpointGroup("Diagnostics")]
	public sealed class GetDiagnostics : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute, "diag")
				.Produces<string>(200, "application/json");
		}

		public async Task<IResult> Execute(HttpContext httpContext)
		{
			return Results.Ok("Diagnostics");
		}
	}
}
