namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	/// <summary>
	///		Extension methods for mapping endpoint handler methods.
	/// </summary>
	[PublicAPI]
	public static class EndpointRouteBuilderExtensions
	{
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
				.WithName(handler.Method.GetEndpointName());
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
				.WithName(handler.Method.GetEndpointName());
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
				.WithName(handler.Method.GetEndpointName());
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
				.WithName(handler.Method.GetEndpointName());
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
				.WithName(handler.Method.GetEndpointName());
		}

		private static bool IsAnonymous(this MethodInfo method)
		{
			char[] invalidChars = new char[] { '<', '>' };
			return method.Name.Any(invalidChars.Contains);
		}

		private static string GetEndpointName(this MethodInfo method)
		{
			Type declaringType = method.DeclaringType;

			ArgumentNullException.ThrowIfNull(declaringType);

			string name = declaringType.GetCustomAttribute<EndpointNameAttribute>()?.Name ?? declaringType.Name;
			return name;
		}
	}
}
