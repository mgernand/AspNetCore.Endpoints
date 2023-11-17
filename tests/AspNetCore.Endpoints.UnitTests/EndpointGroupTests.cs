namespace AspNetCore.Endpoints.UnitTests
{
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Endpoints;

	public class EndpointGroupTests
	{
		[Test]
		[TestCase("")]
		[TestCase("   ")]
		[TestCase(null)]
		public void ShouldThrowIfGroupNameIsEmpty(string? groupName)
		{
			Action action = () =>
			{
				EndpointGroup _ = new EndpointGroup(groupName);
			};

			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCompareByName_NotEqual()
		{
			EndpointGroup endpointGroup1 = new EndpointGroup("One");
			EndpointGroup endpointGroup2 = new EndpointGroup("Two");

			bool equal = endpointGroup1 == endpointGroup2;
			equal.Should().BeFalse();
		}

		[Test]
		public void ShouldCompareByName_Equal()
		{
			EndpointGroup endpointGroup1 = new EndpointGroup("One");
			EndpointGroup endpointGroup2 = new EndpointGroup("One");

			bool equal = endpointGroup1 == endpointGroup2;
			equal.Should().BeTrue();
		}
	}
}
