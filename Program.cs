using Microsoft.EntityFrameworkCore;
using DirectorAPI.Data;
using System.Text.Json.Serialization;


// Add services to the container.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<Sistem21PrimariaContext>(x => x.UseMySql("server=sistemas19.com;database=sistem21_primaria;user=sistem21_primaria;password=sistemas19_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/



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