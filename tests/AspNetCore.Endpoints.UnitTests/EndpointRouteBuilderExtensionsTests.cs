﻿namespace AspNetCore.Endpoints.UnitTests
{
	using System;
	using AspNetCore.Endpoints.UnitTests.Endpoints.Extensions;
	using FluentAssertions;
	using NUnit.Framework;

	public class EndpointRouteBuilderExtensionsTests
	{
		[Test]
		[TestCase("GET")]
		[TestCase("POST")]
		[TestCase("PUT")]
		[TestCase("PATCH")]
		[TestCase("DELETE")]
		public void ShouldThrowWithAnonymousHandler(string method)
		{
			Anon endpoint = new Anon(method);
			Action action = () => endpoint.Map(null);

			action.Should().Throw<NotSupportedException>();
		}
	}
}