namespace MadEyeMatt.AspNetCore.Endpoints
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///		Extension methods for the <see cref="IServiceCollection"/> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///		Adds the endpoint types as services to enable ctor injection.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddEndpoints(this IServiceCollection services)
		{
			return services;
		}
	}
}