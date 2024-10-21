using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using DeviceAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<UserContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DemoContext"))); // add the db to the DI container and Specifies that the database context will use an in-memory database.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7205/") });

var connUser = builder.Configuration.GetConnectionString("UserDbConnString");
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UserContext>(opt => opt.UseNpgsql(connUser));

var connDevice = builder.Configuration.GetConnectionString("DeviceDbConnString");
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DeviceContext>(opt => opt.UseNpgsql(connDevice));

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
