using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task & Inventory Management API",
        Version = "v1.0",
        Description = "REST API built with ASP.NET Core 8.0 for managing products, categories, and operational tasks."
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task & Inventory API v1");
});

app.UseAuthorization();
app.MapControllers();

app.Run();
