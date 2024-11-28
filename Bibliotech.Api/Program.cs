using Bibliotech.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BibliotecaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL_String"));
});

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("EveryBody", app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyHeader();
        app.AllowAnyMethod();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EveryBody");
app.UseAuthorization();

app.MapControllers();

app.Run();
