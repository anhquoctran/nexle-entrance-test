using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NexleInterviewTesting.Infrastructure.DatabaseContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adding EntityFrameworkCore DbContext
builder.Services.AddDbContext<NexleDbContext>(c =>
{
    var connString = builder.Configuration.GetConnectionString("Default");
    c.UseMySql(connString, ServerVersion.AutoDetect(connString), o =>
    {
        o.EnableRetryOnFailure();
    })
    .LogTo(Console.WriteLine, LogLevel.Information);

});

// Custom password handling for ASP.NET Core Identity
builder.Services.AddTransient<IPasswordHasher<User>, BcryptPasswordHasher>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        // Json naming camelCase policy, using System.Text.Json
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Adding UoW and Repositories
builder.Services.AddUnitOfWorkAndRepositories();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//Adding authorization and authentication
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

}

app.Run();
