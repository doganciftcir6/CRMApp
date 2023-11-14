using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Business.CustomDescriber;
using Onicorn.CRMApp.Business.Services.Concrete;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Identity
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 1;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
});

builder.Services.AddScoped<IUow, Uow>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
