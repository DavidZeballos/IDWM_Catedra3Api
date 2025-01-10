using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cat3_API.Src.Data;
using Cat3_API.Src.Services;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables desde .env para la conexión a la base de datos
Env.Load();
var databaseConnection = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// Configuración de la base de datos SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(databaseConnection));

// Configuración de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración de autenticación JWT
// Configuración de autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JWT");
var signingKey = jwtSettings["SigningKey"] 
    ?? throw new InvalidOperationException("JWT SigningKey no configurado en appsettings.json.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
        };
    });


// Registro de servicios personalizados
builder.Services.AddSingleton<CloudinaryService>();

// Configuración de servicios de MVC y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<JwtService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware global
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
