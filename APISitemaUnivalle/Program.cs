using APISitemaUnivalle.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var myCors = "cors";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbUnivalleContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbUnivalle"));
});
builder.Services.AddCors(options =>
{
<<<<<<< HEAD
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
=======
    options.AddPolicy(name: myCors, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod().AllowAnyHeader();
    });
>>>>>>> 2361eb17fcce933ec93c542226f266031886e151
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors(myCors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
