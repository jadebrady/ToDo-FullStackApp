using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")  // Your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.MapControllers();

app.Run();
