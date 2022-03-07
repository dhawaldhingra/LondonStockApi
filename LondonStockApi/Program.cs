using AutoMapper;
using LondonStockApi.DataModels;
using LondonStockApi.Models;
using LondonStockApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// D.I. for Sqllite database
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.AddDbContext<StocksDbContext>(s => s.UseSqlite(@"Data source=.\Database\LondonStockApi.db"));

// D.I. for automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});
// Add Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "London Stock Prices",
        Version = "v1",
        Description = "An API for fetching and updating the stock prices",
        Contact = new OpenApiContact
        {
            Email = "dhawal.dhingra@gmail.com",
            Name = "Dhawal Dhingra",
            Url = new Uri("https://www.linkedin.com/in/dhawaldhingra/")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});


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
