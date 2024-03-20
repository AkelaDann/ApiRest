using ApiRest.Api.Common.Errors;
using ApiRest.Api.Filters;
using ApiRest.Api.Middleware;
using ApiRest.Application;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfraestructure(builder.Configuration);
    // segunda forma de implementar el control de errores FilterAttribute a todos los controladores
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
}

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, ApiRestProblemDetailsFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    //implementacion de control de errores Midldleware
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    //
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}



