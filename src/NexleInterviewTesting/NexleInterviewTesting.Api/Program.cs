using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexleInterviewTesting.Domain.Entities;
using NexleInterviewTesting.Infrastructure.DatabaseContexts;
using NexleInterviewTesting.Infrastructure.Helpers;
using System;
using System.Text.Json;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
