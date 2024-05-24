namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		An attribute to override the default group name for an endpoint.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class EndpointGroupAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="EndpointGroupAttribute"/> type.
		/// </summary>
		/// <param name="group"></param>
		public EndpointGroupAttribute(string group)
		{
			ArgumentException.ThrowIfNullOrEmpty(group);

			this.Group = group;
		}

		/// <summary>
		///		Gets the group name.
		/// </summary>
		public string Group { get; }
	}
}