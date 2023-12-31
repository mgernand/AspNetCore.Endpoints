﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace MadEyeMatt.AspNetCore.Endpoints
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		A named endpoint group.
	/// </summary>
	[PublicAPI]
	public sealed class EndpointGroup : IEquatable<EndpointGroup>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="EndpointGroup"/> type.
		/// </summary>
		/// <param name="groupName"></param>
		public EndpointGroup(string groupName)
		{
#if NET7_0
			if (string.IsNullOrWhiteSpace(groupName))
			{
				throw new ArgumentException(nameof(groupName));
			}
#else
			ArgumentException.ThrowIfNullOrWhiteSpace(groupName);
#endif

			this.Name = groupName;
		}

		/// <summary>
		///		Gets the group groupName.
		/// </summary>
		public string Name { get; }

		/// <inheritdoc />
		public bool Equals(EndpointGroup other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(this.Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is EndpointGroup other && this.Equals(other);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return (this.Name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.Name) : 0);
		}

		public static bool operator ==(EndpointGroup left, EndpointGroup right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EndpointGroup left, EndpointGroup right)
		{
			return !Equals(left, right);
		}
	}
}