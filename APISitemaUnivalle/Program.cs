using APISitemaUnivalle.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var myCors = "cors";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => 
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.WriteIndented = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbUnivalleContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbUnivalle"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod().AllowAnyHeader();
    });
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
