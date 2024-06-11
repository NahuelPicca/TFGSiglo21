using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PeliculasAPI.BehaviorBadRequests;
using PeliculasAPI.Filtros;
using RossiEventos;
using RossiEventos.Utilidades;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var myDirectionOriginal = "directionName";
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

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    opt =>
    {
        //Password
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireNonAlphanumeric = true;

        //Require Email confirmed
        opt.SignIn.RequireConfirmedEmail = false;

        //Bloqueo de cuenta
        opt.Lockout.AllowedForNewUsers = true;
        opt.Lockout.MaxFailedAccessAttempts = 5;
    })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(opc => opc.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"])),
           ClockSkew = TimeSpan.Zero
       });

//Establece la autorizacion para el usuario EsAdmin para que tenga acceso a todo.
builder.Services.AddAuthorization(opc => opc.AddPolicy("EsAdmin", pol => pol.RequireClaim("role", "admin")));

builder.Services.AddCors(opt =>
{
    // var frontUrl = configuration.GetValue<string>("frontend_url");
    //Permite que escuche a cualquier direccion de API 
    opt.AddPolicy(myDirectionOriginal, p => p.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader()
                                             .WithExposedHeaders(new string[] { "cantidadTotalRegistros" }));
});

builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
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
app.UseRouting();
app.UseCors(myDirectionOriginal);
app.UseAuthorization();

app.MapControllers();

app.Run();
