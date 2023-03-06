using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexleInterviewTesting.Api;
using NexleInterviewTesting.Application.Middleware;
using NexleInterviewTesting.Infrastructure.DatabaseContexts;
using NexleInterviewTesting.Infrastructure.Helpers;
using System.IO;
using System.Linq;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Custom validation response
builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
    apiBehaviorOptions.InvalidModelStateResponseFactory = (context) =>
    {
        var modelState = context.ModelState.ToDictionary(x => JsonNamingPolicy.CamelCase.ConvertName(x.Key), x => x.Value.Errors.Select(x => x.ErrorMessage).ToArray());
        return new BadRequestObjectResult(BaseResponse<object>.BadRequest(modelState));
    }
);

// Adding EntityFrameworkCore DbContext
builder.Services.AddNexleDbContext(builder.Configuration);

// Custom auth
builder.Services.ConfigureCustomAuth(builder.Configuration);

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

app.UseHttpExceptionMiddleware();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var json = ResponseUtils.GetJsonResponseString(HttpStatusCode.InternalServerError);
            await context.Response.WriteJson(json);
        }
    });
});

app.Run();
