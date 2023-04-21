using Application;
using Infrastructure;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
