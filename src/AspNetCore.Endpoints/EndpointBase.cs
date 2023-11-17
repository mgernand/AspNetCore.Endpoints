namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		An abstract base class for a single endpoint.
	/// </summary>
	[PublicAPI]
	public abstract class EndpointBase
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="EndpointBase"/> type.
		/// </summary>
		protected EndpointBase()
		{
			Type groupType = this.GetType();

			string groupName = groupType.GetCustomAttribute<EndpointGroupAttribute>()?.GroupName?.Trim() ?? 
			                   groupType.Namespace?.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();

			ArgumentException.ThrowIfNullOrWhiteSpace(groupName);

			this.Group = new EndpointGroup(groupName);
		}

		/// <summary>
		///		Gets the endpoint group.
		/// </summary>
		public EndpointGroup Group { get; }

		/// <summary>
		///		Maps the endpoint.
		/// </summary>
		/// <param name="endpoints"></param>
		public abstract void Map(IEndpointRouteBuilder endpoints);
	}
}