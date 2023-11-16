namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		An attribute to override the default route handler of an endpoint.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class EndpointNameAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="EndpointNameAttribute"/> type.
		/// </summary>
		/// <param name="handlerName"></param>
		public EndpointNameAttribute(string handlerName)
		{
			this.HandlerName = handlerName;
		}

		/// <summary>
		///		Gets the handler name.
		/// </summary>
		public string HandlerName { get; }
	}
}