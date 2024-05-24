namespace MadEyeMatt.AspNetCore.Endpoints
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		An abstract base class for a single endpoint.
	/// </summary>
	[PublicAPI]
	public abstract class EndpointBase : IEndpoint
	{
		/// <summary>
		///		Maps the endpoint.
		/// </summary>
		/// <param name="endpoints"></param>
		public abstract void Map(IEndpointRouteBuilder endpoints);
	}
}