using Microsoft.EntityFrameworkCore;
using UserAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7205/") });

var connUser = builder.Configuration.GetConnectionString("UserDbConnString");
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UserContext>(opt => opt.UseNpgsql(connUser));

//var connDevice = builder.Configuration.GetConnectionString("DeviceDbConnString");
//builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DeviceContext>(opt => opt.UseNpgsql(connDevice));

builder.Services.AddCors(
    op => {
        op.AddPolicy("AllowAll", p =>
        {
            p.WithOrigins("http://localhost/frontend");
            p.AllowAnyHeader();
            p.AllowAnyMethod();
            p.AllowAnyOrigin();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
