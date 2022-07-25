using Amazon.Runtime;
using Amazon.S3;
using Amazon.Rekognition;
using AulaAWS.Lib.Data;
using AulaAWS.Lib.Data.Repositorios;
using AulaAWS.Lib.Data.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using AulaAWS.Application.Services;
using AulaAWS.Application;
using AulaAWS.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUsuarioApplication, UsuarioApplication>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<UsuarioMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
