namespace AspNetCore.Endpoints.UnitTests
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Hosting.Server;
	using Microsoft.AspNetCore.Mvc.Testing.Handlers;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	public abstract class TestServerFixtureBase
	{
		private readonly CancellationTokenSource cancellationTokenSource;
		private readonly List<HttpClient> clients;

		private WebApplication? app;
		private TestServer? server;

		protected TestServerFixtureBase()
		{
			this.cancellationTokenSource = new CancellationTokenSource();
			this.clients = new List<HttpClient>();
		}

		protected HttpClient CreateClient()
		{
			return this.CreateClientInternal(new RedirectHandler(7), new CookieContainerHandler());
		}

		protected abstract Task ConfigureServices(WebApplicationBuilder builder);

		protected abstract Task Configure(WebApplication app);

		[OneTimeSetUp]
		public async Task OneTimeSetUp()
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(
				new WebApplicationOptions
				{
					EnvironmentName = Environments.Development
				});

			builder.WebHost
				.UseShutdownTimeout(TimeSpan.FromSeconds(5))
				.UseTestServer();

			await this.ConfigureServices(builder);

			this.app = builder.Build();

			await this.Configure(this.app);

			this.server = (TestServer)this.app.Services.GetRequiredService<IServer>();

			await this.app.StartAsync(this.cancellationTokenSource.Token);
		}

		[OneTimeTearDown]
		public async Task OneTimeTearDown()
		{
			foreach (HttpClient client in this.clients)
			{
				client.Dispose();
			}

#if NET8_0
			await this.cancellationTokenSource.CancelAsync();

#else
			this.cancellationTokenSource.Cancel();
#endif
			this.cancellationTokenSource.Dispose();

			if (this.app is not null)
			{
				await this.app.StopAsync(TimeSpan.FromSeconds(30));
				await this.app.DisposeAsync();
			}

			this.server?.Dispose();
		}

		private HttpClient CreateClientInternal(params DelegatingHandler[] handlers)
		{
			if (this.server is null)
			{
				throw new InvalidOperationException("The TestServer is not initialized.");
			}

			HttpClient client;

			if (handlers.Length == 0)
			{
				client = this.server.CreateClient();
			}
			else
			{
				for (int i = handlers.Length - 1; i > 0; i--)
				{
					handlers[i - 1].InnerHandler = handlers[i];
				}

				HttpMessageHandler serverHandler = this.server.CreateHandler();
				handlers[^1].InnerHandler = serverHandler;

				client = new HttpClient(handlers[0]);
			}

			this.clients.Add(client);

			client.BaseAddress = this.server.BaseAddress;

			return client;
		}
	}
}
