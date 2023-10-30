using CashCrewAPI.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Services.Contracts;
using WebApi.Extensions;
using Presentation.ActionFilters;
using Microsoft.JSInterop;
using CashCrewAPI.Runtime.Auth.Config;
using CashCrewAPI.Runtime.Auth.Store;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
Console.Write(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    //config.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });
})
.AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.ConfigureSwagger();
builder.Services.ConfigurePostgresContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();

// IdentityServer  nad Authentication
var secretForIntrospectionEndpoint = builder.Configuration["AppSettings:SecretForIntrospectionEndpoint"];

var auth = builder.Services.AddIdentityServer();
auth.AddDeveloperSigningCredential();
//auth.AddSigningCredential(new X509Certificate2(Path.Combine(Environment.CurrentDirectory, "Assets/Certificates", "AuthSigningCredential.pfx"), "", X509KeyStorageFlags.MachineKeySet));
auth.AddResourceOwnerValidator<CashCrewAPI.Runtime.Validators.ValidationFilterAttribute>();
auth.AddInMemoryApiResources(ApiResourceConfig.GetApiResources(secretForIntrospectionEndpoint));
auth.AddClientStore<AuthClientStore>();
auth.AddCorsPolicyService<CashCrewAPI.Runtime.Services.CorsPolicyAllowAllService>();

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(options =>
{
    options.Authority = builder.Configuration["AppSettings:AuthorityUrl"];
    options.RequireHttpsMetadata = false;
    options.ApiName = "auth.api";

    options.JwtBearerEvents = new JwtBearerEvents { OnTokenValidated = OnTokenValidated, OnAuthenticationFailed = OnAuthenticationFailed };
});

//


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger); // For exception middleware


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();


app.Run();


static Task OnTokenValidated(TokenValidatedContext tokenValidatedContext)
{
    return Task.CompletedTask;
}

static Task OnAuthenticationFailed(AuthenticationFailedContext arg)
{
    return Task.CompletedTask;
}
