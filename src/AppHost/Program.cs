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

// Postgres server and database (added)
//var pgUsername = builder.AddParameter("pgUsername", secret: true);
//var pgPassword = builder
//    .AddParameter("pgPassword", "postgres-password", true);

//var postgresServer = builder
//    .AddPostgres(Services.PostgresServer, pgUsername, pgPassword, Services.PostgresPort)
//    .WithLifetime(ContainerLifetime.Persistent)
//    .WithReferenceEnvironment(ReferenceEnvironmentInjectionFlags.ConnectionString);

//var postgresDb = postgresServer
//    .AddDatabase(Services.PostgresDatabase);

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

// Additional web project that connects to the Postgres database
//var webPostgres = builder.AddProject<Projects.Web>(Services.PgWebApi)
//    .WithReference(postgresDb)
//    .WaitFor(postgresServer)
//    .WithExternalHttpEndpoints()
//    .WithAspNetCoreEnvironment()
//    .WithUrlForEndpoint("http", url =>
//    {
//        url.DisplayText = "Postgres Scalar API Reference";
//        url.Url = "/scalar-postgres";
//    });

if (builder.ExecutionContext.IsRunMode)
{
    builder.AddJavaScriptApp(Services.WebFrontend, "./../Web/ClientApp")
        .WithRunScript("start")
        .WithReference(web)
        .WaitFor(web)
        .WithHttpEndpoint(env: "PORT")
        .WithExternalHttpEndpoints();

    //builder.AddJavaScriptApp(Services.PgWebFrontend, "./../Web/ClientApp")
    //    .WithRunScript("start")
    //    .WithReference(web)
    //    .WaitFor(web)
    //    .WithHttpEndpoint(env: "PORT")
    //    .WithExternalHttpEndpoints();
}

builder.Build().Run();
