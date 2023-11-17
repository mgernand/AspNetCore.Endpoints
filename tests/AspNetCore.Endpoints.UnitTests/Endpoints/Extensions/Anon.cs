namespace AspNetCore.Endpoints.UnitTests.Endpoints.Extensions
{
	using System.Net;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Endpoints;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;

	[PublicAPI]
	public sealed class Anon : EndpointBase
	{
		private readonly string method;

		public Anon()
		{
		}

		// Note: This is just for testing purposes. Endpoints need a parameter-less ctor.
		public Anon(string method)
		{
			this.method = method;
		}

		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			switch (this.method)
			{
				case "GET":
					endpoints.MapGet((HttpContext _) => Results.Ok());
					break;
				case "POST":
					endpoints.MapPost((HttpContext _) => Results.Ok());
					break;
				case "PUT":
					endpoints.MapPut((HttpContext _) => Results.Ok());
					break;
				case "PATCH":
					endpoints.MapPatch((HttpContext _) => Results.Ok());
					break;
				case "DELETE":
					endpoints.MapDelete((HttpContext _) => Results.Ok());
					break;
			}
		}
	}
}