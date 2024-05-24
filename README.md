# AspNetCore.Endpoints

A library that helps in building and configuring object-oriented minimal API endpoints.

Mapping every single minimal API endpoint in the ```Program.cs``` file can become 
confusing very fast. For applications hosting a larger amount of endpoints this 
library allows to implement an endpoint in a structured way. This endpoints will
then be automatically mapped, removing clutter from the ```Program.cs``` file.

Everything related to and endpoint like the mapping and the implementation itself
is encapsulated in a single class. The library offers a default way of naming groups
and endpoints. The default convention for the group name an endpoint belongs to is
the last part of the namespace the endpoint belongs to. The default convention for
the endppoint name id the class name of the endpoint.

This default conventions can be overridden by using attributes athe endpoints class level.

- ```[EndpointGroup("GroupName")]```
    
    The name of the group this endpoint belongs to.

- ```[EndpointName("SomeOtherName")]```

    The name of the endpoint.

## Endpoints Usage

Every endpoint is implemented in it's own class, deriving from ```EndpointBase```. 
Endpoints are discovered from the available types using this base class.

```C#

	public sealed class Get : EndpointBase
	{
		/// <inheritdoc />
		public override void Map(IEndpointRouteBuilder endpoints)
		{
			endpoints
				.MapGet(this.Execute, "{id}")
				.AllowAnonymous()
				.Produces<Customer>(200, "application/json");
		}

		public async Task<IResult> Execute(HttpContext httpContext, string id)
		{
			return Results.Ok(new Customer
			{
				Name = "John Connor"
			});
		}
	}

```

The mapping and configuration of additional meta data configuration of the endpoint is done 
in the ```Map``` method. The actual endpoint implementation is done in the ```Execute``` method.
The name of the method free to choose, it doesn't affect the mapping of the endpoint in any way.

To allow the endpoint beeing automatically mapped one has to add this to the ```Program.cs```:

```C#

app.MapEndpoints();

```

This it!

## Additional Configuration

The endpoints are by default mapped under a global route prefix ```api```. To change this
default value, one can configure the ```EndpointsOptions``` when configuring the application.

```C#

	builder.Services.Configure<EndpointsOptions>(options =>
	{
		options.EndpointsRoutePrefix = "endpoints";
		options.MapGroup = groupBuilder =>
		{
			groupBuilder.WithOpenApi();
		};
	});

```

In this exampple the global route prefix for all enpoints is changed to ```endpoints``` and
an additional endpoint group configuration is added using the ```MapGroup``` callback,
which is called for every endpoint group.
