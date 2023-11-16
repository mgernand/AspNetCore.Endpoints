namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		Extension methods for the <see cref="WebApplication"/> type.
	/// </summary>
	[PublicAPI]
	public static class WebApplicationExtensions
	{
		/// <summary>
		///		Maps all <see cref="Endpoint"/> implementations from registered application parts.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static WebApplication MapEndpoints(this WebApplication app)
		{
			Type endpointBaseType = typeof(EndpointBase);

			IEnumerable<Type> types = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(assembly => assembly.GetExportedTypes().Where(type => type.IsSubclassOf(endpointBaseType)))
				.Distinct();

			IList<EndpointBase> endpoints = new List<EndpointBase>();

			foreach (Type type in types)
			{
				EndpointBase endpoint = Activator.CreateInstance(type) as EndpointBase;
				endpoints.Add(endpoint);
			}

			foreach (IGrouping<EndpointGroup, EndpointBase> grouping in endpoints.GroupBy(x => x.Group))
			{
				//RouteGroupBuilder groupEndpoints = app.MapGroup(grouping.Key);
				foreach (EndpointBase endpoint in grouping)
				{
					endpoint.Map(app);
				}
			}

			return app;
		}

		/// <summary>
		///		Maps the given endpoint group with default settings.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="group"></param>
		/// <returns></returns>
		private static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroup group)
		{
			ArgumentNullException.ThrowIfNull(group);

			string groupName = group.Name.ToLowerInvariant();

			return app
				.MapGroup($"/api/{groupName}")
				.WithGroupName(groupName)
				.WithTags(groupName);
		}
	}
}
