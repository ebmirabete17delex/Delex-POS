using Delex_POS.Shared;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureContainerAppEnvironment("aca-env");

var dbPassword = builder
    .AddParameter("password","password", true);

var mySQLServer = builder
    .AddMySql(Services.DatabaseServer, dbPassword, Services.DbPort)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithReferenceEnvironment(ReferenceEnvironmentInjectionFlags.ConnectionString);


var mySQLDb = mySQLServer
    .AddDatabase(Services.Database);

var web = builder.AddProject<Projects.Web>(Services.WebApi)
    .WithReference(mySQLDb)
    .WaitFor(mySQLServer)
    .WithExternalHttpEndpoints()
    .WithAspNetCoreEnvironment()
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar API Reference";
        url.Url = "/scalar";
    });
    
if (builder.ExecutionContext.IsRunMode)
{
    builder.AddJavaScriptApp(Services.WebFrontend, "./../Web/ClientApp")
        .WithRunScript("start")
        .WithReference(web)
        .WaitFor(web)
        .WithHttpEndpoint(env: "PORT")
        .WithExternalHttpEndpoints();
}

builder.Build().Run();
