namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		An attribute to override the default endpoint name.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class EndpointNameAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="EndpointNameAttribute"/> type.
		/// </summary>
		/// <param name="name"></param>
		public EndpointNameAttribute(string name)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);

			this.Name = name;
		}

		/// <summary>
		///		Gets the endpoint name.
		/// </summary>
		public string Name { get; }
	}
}