# ASPNETCORE_learning_notes

<h2>Program.cs</h2>

Program.cs file is where:
- services required by the app are configured
- the apps request handling pipeline is defined as a series of middleware components

<h2>Dependency injection (services) </h2>

- dependency ijection (DI) makes configured services available troughout an app
- services are added to the DI container with builder.Services

- registrace vlastbích services pomocí 3 možností:

  - AddSingleton -> existuje po celou dobu běhu aplikace = využívá resources
  - AddScoped    -> existuje po dobu http requestu, nová instance je vytvořena pro každý request, vhodné pro API controller
  - AddTransient -> service je vytvořen a zničen jakmile je metoda u konce, není vhodné pro http requesty

<b>In .NET's dependency injection there are three major lifetimes:</b>
  
1. Singleton which creates a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.

2. Scoped lifetime services are created once per request within the scope. It is equivalent to a singleton in the current scope. For example, in MVC it creates one instance for each HTTP request, but it uses the same instance in the other calls within the same web request.

3. Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.

<h2>Middleware</h2>

- the request handling pipeline is composed of a series of middleware components
- each component performs an operation on <b>HttpContext</b> and either invokens the next middleware in the pipeline or terminates the request
- by convetion middleware is added to the pipeline by invoking a "Use{Feature}" extension method

example middleware:

- app.UseHttpsRedirection();
- app.UseStaticFiles();
- app.UseAuthorization();

<h2>Host</h2>

- on startup, aspnetcore build a host
- host encapsulate all of the apps resources such as:
  - http server implementation
  - middleware components
  - logging
  - dependency injection
  - configuration
  
TBC
