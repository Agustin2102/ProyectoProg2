using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//Script para cargar las variables de entorno del archivo .env
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

//añado los controladores
builder.Services.AddControllers();

//Creacion de la cadena de conexión a partir de las variables del sistema
var connetionString = builder.Configuration.GetConnectionString("cnClinica");
connetionString = connetionString.Replace("SERVER_NAME", builder.Configuration["SERVER_NAME"]);
//connetionString = connetionString.Replace("DB_NAME", builder.Configuration["DB_NAME"]);
connetionString = connetionString.Replace("DB_USER", builder.Configuration["DB_USER"]);
connetionString = connetionString.Replace("DB_PASS", builder.Configuration["DB_PASS"]);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    // Configuración básica de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tu API", Version = "v1" });

    // Configuración de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT en este formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



//Añado la conexion a la BD
builder.Services.AddSqlServer<ClinicaContext>(builder.Configuration.GetConnectionString("cnClinica"));
/*------AÑADIR LOS CONTENEDORES PARA LA INYECCION DE DEPENDENCIAS-----*/
builder.Services.AddScoped<IDoctorService, DoctorDbService>();



// Configurar el contexto para Identity (autenticación y autorización)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnClinica")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar JWT para autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Valida que el emisor del token sea el esperado
        ValidateAudience = true, // Valida que la audiencia del token sea la esperada
        ValidateLifetime = true, // Valida que el token no haya expirado
        ValidateIssuerSigningKey = true, // Verifica que el token esté firmado con la clave correcta
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Especifica el emisor esperado del token
        ValidAudience = builder.Configuration["Jwt:Audience"], // Especifica la audiencia esperada del token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Clave secreta para firmar el token
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


/*
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.8
*/
