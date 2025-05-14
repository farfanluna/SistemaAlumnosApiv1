using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaAlumnosApi.Data;
using SistemaAlumnosApi.Repositories.Interfaces;
using SistemaAlumnosApi.Repositories.Sql;        
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para permitir solicitudes 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        cors => cors
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add services to the container.
builder.Services.AddControllers();

// Registrar repositorios (Interfaces + implementaciones)
builder.Services.AddScoped<IAlumnoRepository, SqlAlumnoRepository>();
builder.Services.AddScoped<IMateriaRepository, SqlMateriaRepository>();
builder.Services.AddScoped<IAsignacionRepository, SqlAsignacionRepository>();
builder.Services.AddScoped<IExamenRepository, SqlExamenRepository>();
builder.Services.AddScoped<IPreguntaRepository, SqlPreguntaRepository>();
builder.Services.AddScoped<IRespuestaRepository, SqlRespuestaRepository>();
builder.Services.AddScoped<ICalificacionRepository, SqlCalificacionRepository>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
var key = builder.Configuration["Jwt:key"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
