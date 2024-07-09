namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///		Extension methods for the <see cref="IEndpointRouteBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class EndpointRouteBuilderExtensions
	{
		/// <summary>
		///		Maps all <see cref="Endpoint"/> implementations from registered application parts.
		/// </summary>
		/// <param name="builder">The endpoint route builder to map the endpoints with.</param>
		/// <returns>The endpoint route builder.</returns>
		public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
		{
			IEnumerable<Type> types = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(assembly => assembly.GetExportedTypes().Where(type => type.IsSubclassOf(typeof(EndpointBase))))
				.Distinct();

			IList<EndpointBase> endpoints = new List<EndpointBase>();

			foreach (Type type in types)
			{
				EndpointBase endpoint = ActivatorUtilities.CreateInstance(builder.ServiceProvider, type) as EndpointBase;
				endpoints.Add(endpoint);
			}

			EndpointsOptions options = builder.ServiceProvider.GetRequiredService<IOptions<EndpointsOptions>>().Value;
			string globalPrefix = options.EndpointsRoutePrefix?.Trim('/') ?? "api";

			RouteGroupBuilder groupBuilder = builder.MapGroup(globalPrefix);

			foreach (EndpointBase endpoint in endpoints)
			{
				endpoint.Map(groupBuilder);
			}

			return builder;
		}

		/// <summary>
		///		Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP GET requests
		///		for the specified pattern.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
		/// <param name="handler">The delegate executed when the endpoint is matched.</param>
		/// <param name="pattern">The route pattern.</param>
		/// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
		public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder endpoints, Delegate handler, [StringSyntax("Route")] string pattern = "")
		{
			if (handler.Method.IsAnonymous())
			{
				throw new NotSupportedException("Anonymous handlers are not supported by this mapping method.");
			}

			return endpoints
				.MapGet(pattern, handler)
				.WithName(handler)
				.WithTags(handler);
		}

		/// <summary>
		///		Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP POST requests
		///		for the specified pattern.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
		/// <param name="pattern">The route pattern.</param>
		/// <param name="handler">The delegate executed when the endpoint is matched.</param>
		/// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
		public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder endpoints, Delegate handler, [StringSyntax("Route")] string pattern = "")
		{
			if (handler.Method.IsAnonymous())
			{
				throw new NotSupportedException("Anonymous handlers are not supported by this mapping method.");
			}

			return endpoints
				.MapPost(pattern, handler)
				.WithName(handler)
				.WithTags(handler);
		}

		/// <summary>
		///		Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP PUT requests
		///		for the specified pattern.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
		/// <param name="pattern">The route pattern.</param>
		/// <param name="handler">The delegate executed when the endpoint is matched.</param>
		/// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
		public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder endpoints, Delegate handler, [StringSyntax("Route")] string pattern = "")
		{
			if (handler.Method.IsAnonymous())
			{
				throw new NotSupportedException("Anonymous handlers are not supported by this mapping method.");
			}

			return endpoints
				.MapPut(pattern, handler)
				.WithName(handler)
				.WithTags(handler);
		}

		/// <summary>
		///		Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP PATCH requests
		///		for the specified pattern.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
		/// <param name="pattern">The route pattern.</param>
		/// <param name="handler">The <see cref="Delegate" /> executed when the endpoint is matched.</param>
		/// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
		public static RouteHandlerBuilder MapPatch(this IEndpointRouteBuilder endpoints, Delegate handler, [StringSyntax("Route")] string pattern = "")
		{
			if (handler.Method.IsAnonymous())
			{
				throw new NotSupportedException("Anonymous handlers are not supported by this mapping method.");
			}

			return endpoints
				.MapPatch(pattern, handler)
				.WithName(handler)
				.WithTags(handler);
		}

		/// <summary>
		///		Adds a <see cref="RouteEndpoint"/> to the <see cref="IEndpointRouteBuilder"/> that matches HTTP DELETE requests
		///		for the specified pattern.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
		/// <param name="pattern">The route pattern.</param>
		/// <param name="handler">The delegate executed when the endpoint is matched.</param>
		/// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
		public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder endpoints, Delegate handler, [StringSyntax("Route")] string pattern = "")
		{
			if (handler.Method.IsAnonymous())
			{
				throw new NotSupportedException("Anonymous handlers are not supported by this mapping method.");
			}

			return endpoints
				.MapDelete(pattern, handler)
				.WithName(handler)
				.WithTags(handler);
		}

		private static bool IsAnonymous(this MethodInfo method)
		{
			char[] invalidChars = ['<', '>'];
			return method.Name.Any(invalidChars.Contains);
		}

		private static RouteHandlerBuilder WithName(this RouteHandlerBuilder builder, Delegate handler)
		{
			EndpointNameAttribute endpointNameAttribute = handler.Method.DeclaringType?.GetCustomAttribute<EndpointNameAttribute>();
			string name = endpointNameAttribute is not null
				? endpointNameAttribute.Name
				: handler.Method.DeclaringType?.Name ?? "Endpoints";

			return builder.WithName(name);
		}

		private static RouteHandlerBuilder WithTags(this RouteHandlerBuilder builder, Delegate handler)
		{
			EndpointGroupAttribute endpointGroupAttribute = handler.Method.DeclaringType?.GetCustomAttribute<EndpointGroupAttribute>();
			if (endpointGroupAttribute is not null)
			{
				builder = builder.WithTags(endpointGroupAttribute.Group);
			}

			return builder;
		}
	}
}
