global using UserManagementAPI.Models;
global using UserManagementAPI.Data;
using UserManagementAPI.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => 
    x.UseSqlServer(builder.Configuration.GetConnectionString("default"))
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

app.UseCors(option =>
    option.WithOrigins(
        "http://localhost:3000",
        "http://localhost:8080",
        "http://localhost:9191"
    )
    .AllowAnyMethod()
    .AllowAnyHeader()
);

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
