using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .WithMethods("GET", "PUT", "POST", "DELETE");
                      });
});

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);




var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();
await app.UseOcelot();

app.Run();
