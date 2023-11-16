namespace SampleApplication.Endpoints.Customers
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class Customer
	{
		/// <summary>
		///		Gets or sets the name.
		/// </summary>
		public string Name { get; set; }
	}
}