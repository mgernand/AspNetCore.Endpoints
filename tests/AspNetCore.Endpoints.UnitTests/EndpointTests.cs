namespace AspNetCore.Endpoints.UnitTests
{
	using AspNetCore.Endpoints.UnitTests.Endpoints.AnotherGroup;
	using FluentAssertions;

	public class EndpointTests
	{
		[Test]
		public void ShouldDeriveGroupNameFromNamespace()
		{
			GroupNameFromNamespace endpoint = new GroupNameFromNamespace();

			endpoint.Group.Name.Should().Be("AnotherGroup");
		}

		[Test]
		public void ShouldDeriveGroupNameFromAttribute()
		{
			GroupNameFromAttribute endpoint = new GroupNameFromAttribute();

			endpoint.Group.Name.Should().Be("AttributeGroupName");
		}

		[Test]
		public void ShouldDeriveGroupNameFromNamespaceIfAttributeHasEmptyName()
		{
			EmptyGroupNameFromAttribute endpoint = new EmptyGroupNameFromAttribute();
			
			endpoint.Group.Name.Should().Be("AnotherGroup");
		}
	}
}