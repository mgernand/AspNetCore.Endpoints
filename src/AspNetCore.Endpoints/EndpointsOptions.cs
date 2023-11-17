namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		The options for the automatic endpoints mapping.
	/// </summary>
	[PublicAPI]
	public sealed class EndpointsOptions
	{
		/// <summary>
		///		Gets or sets the route prefix for all endpoints.
		/// </summary>
		public string EndpointsRoutePrefix { get; set; } = "api";

		/// <summary>
		///		Gets or sets a callback that is used to add additional configuration to route groups.
		/// </summary>
		public Action<RouteGroupBuilder> MapGroup { get; set; }
	}
}