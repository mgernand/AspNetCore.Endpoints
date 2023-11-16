namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		An attribute to override the default group name for an endpoint.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class RouteGroupAttribute : Attribute
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="RouteGroupAttribute"/> type.
		/// </summary>
		/// <param name="groupName"></param>
		public RouteGroupAttribute(string groupName)
		{
			this.GroupName = groupName;
		}

		/// <summary>
		///		Gets the group name.
		/// </summary>
		public string GroupName { get; }
	}
}