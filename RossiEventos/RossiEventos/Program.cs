using Microsoft.EntityFrameworkCore;
using PeliculasAPI.BehaviorBadRequests;
using PeliculasAPI.Filtros;
using RossiEventos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddControllers(o =>
{
    o.Filters.Add(typeof(FiltroDeExcepcion));
    o.Filters.Add(typeof(ParsearBadRequests));
}).ConfigureApiBehaviorOptions(BehaviorBadRequest.Parsear);

builder.Services.AddAutoMapper(typeof(Program));
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("RossiEventoDb")));
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
