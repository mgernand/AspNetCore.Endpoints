namespace MadEyeMatt.AspNetCore.Endpoints
{
	using JetBrains.Annotations;

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
	}
}