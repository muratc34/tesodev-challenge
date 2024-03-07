using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

configuration.AddJsonFile("ocelot.global.json", false);
configuration.AddJsonFile($"ocelot.global.{environment.EnvironmentName}.json", true);

configuration.AddJsonFile("ocelot.routing.json", false);
configuration.AddJsonFile($"ocelot.routing.{environment.EnvironmentName}.json", true);

configuration.AddJsonFile("ocelot.swagger.json", false);
configuration.AddJsonFile($"ocelot.swagger.{environment.EnvironmentName}.json", true);

var appSettings = configuration.GetSection("AppSettings").Get<APIGateway.Contracts.AppSetttings>();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(b =>
    {
        if (appSettings!.Cors?.Origins?.Any() == true) b.WithOrigins(appSettings.Cors.Origins);
        else b.AllowAnyOrigin();

        if (appSettings.Cors?.Methods?.Any() == true) b.WithMethods(appSettings.Cors.Methods);
        else b.AllowAnyMethod();

        if (appSettings.Cors?.Headers?.Any() == true) b.WithHeaders(appSettings.Cors.Headers);
        else b.AllowAnyHeader();
    });
});

if (appSettings!.Swagger.UIRendering)
{
    builder.Services.AddSwaggerForOcelot(configuration, opt =>
    {
        opt.GenerateDocsForAggregates = true;
    });
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

if (appSettings.Swagger.UIRendering)
{
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.DownstreamSwaggerEndPointBasePath = appSettings.Swagger.DownstreamSwaggerEndPointBasePath;
        opt.PathToSwaggerGenerator = appSettings.Swagger.PathToSwaggerGenerator;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

await app.UseOcelot();

app.Run();
