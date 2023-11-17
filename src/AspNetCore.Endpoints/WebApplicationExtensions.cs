namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

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
				RouteGroupBuilder groupEndpoints = app.MapGroup(grouping.Key);
				foreach (EndpointBase endpoint in grouping)
				{
					endpoint.Map(groupEndpoints);
				}
			}

			return app;
		}

		private static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroup group)
		{
			ArgumentNullException.ThrowIfNull(group);

			EndpointsOptions options = app.Services.GetRequiredService<IOptions<EndpointsOptions>>().Value;
			string globalPrefix = options.EndpointsRoutePrefix?.Trim('/');

			string prefix = string.IsNullOrWhiteSpace(globalPrefix) 
				? $"/{group.Name.ToLowerInvariant()}" 
				: $"/{globalPrefix}/{group.Name.ToLowerInvariant()}";

			return app
				.MapGroup(prefix)
				.WithTags(group.Name);
		}
	}
}
