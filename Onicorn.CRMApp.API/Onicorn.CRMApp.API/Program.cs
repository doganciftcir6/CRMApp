using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Business.CustomDescriber;
using Onicorn.CRMApp.Business.DependencyResolvers.Microsoft;
using Onicorn.CRMApp.Business.Services.Concrete;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Ioc Extension Ýmplement
builder.Services.AddDependencies(builder.Configuration);


builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
