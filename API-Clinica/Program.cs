using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

//Nueva Migracion: dotnet ef migrations add ClinicaDb2 --context ClinicaContext


//dotnet ef migrations add ClinicaDb3 --context ClinicaContext
//dotnet ef database update --context ClinicaContext
//dotnet ef database update --context ApplicationDbContext



// Script para cargar las variables de entorno del archivo .env
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

// Añado los controladores
builder.Services.AddControllers();

// Creación de la cadena de conexión a partir de las variables del sistema
var connectionString = builder.Configuration.GetConnectionString("cnClinica");
connectionString = connectionString.Replace("SERVER_NAME", builder.Configuration["SERVER_NAME"]);
// connectionString = connectionString.Replace("DB_NAME", builder.Configuration["DB_NAME"]);
connectionString = connectionString.Replace("DB_USER", builder.Configuration["DB_USER"]);
connectionString = connectionString.Replace("DB_PASS", builder.Configuration["DB_PASS"]);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tu API", Version = "v1" });

    //para que AppointmentStatus sea tratado como una cadena!! con valores específicos en la documentación de Swagger
    c.MapType<Appointment.AppointmentStatus>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = new List<IOpenApiAny>
        {
            new OpenApiString("Scheduled"),
            new OpenApiString("Completed"),
            new OpenApiString("Canceled"),
            new OpenApiString("Rescheduled")
        }
    });

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



// Añado la conexión a la BD
builder.Services.AddSqlServer<ClinicaContext>(builder.Configuration.GetConnectionString("cnClinica"));

/*------AÑADIR LOS CONTENEDORES PARA LA INYECCIÓN DE DEPENDENCIAS-----*/
builder.Services.AddScoped<IDoctorService, DoctorDbService>();
//builder.Services.AddScoped<AccountDbService, AccountDbService>();

//Para Specialty
builder.Services.AddScoped<ISpecialtyService, SpecialtyDbService>();


// Registra IAppointmentService y su implementación
builder.Services.AddScoped<IAppointmentService, AppointmentDbService>(); 

// Registra IAdministradorService y su implementación
builder.Services.AddScoped<IAdministratorService, AdministratorDbService>();

// Registra IPacienteService y su implementación
builder.Services.AddScoped<IPatientService, PatientDbService>();


// Agrega IHttpContextAccessor al contenedor de servicios
builder.Services.AddHttpContextAccessor();
// Agrega AccountDbService al contenedor de servicios
builder.Services.AddScoped<AccountDbService>();


/*
Resumen de los cambios realizados:
builder.Services.AddHttpContextAccessor();: Esto permite que IHttpContextAccessor esté disponible en el contenedor de inyección de dependencias, permitiendo que AccountDbService acceda al contexto HTTP y, por lo tanto, a los claims del usuario autenticado.

builder.Services.AddScoped<AccountDbService>();: Registra AccountDbService en el contenedor, lo que permitirá que este servicio se inyecte en los controladores y se utilice en las solicitudes.

app.UseAuthentication(); y app.UseAuthorization();: Asegúrate de que UseAuthentication y UseAuthorization estén habilitados en el middleware para que los claims del usuario se obtengan correctamente.

Con estas modificaciones, AccountDbService debería estar listo para acceder al contexto HTTP y a los claims del usuario autenticado.

*/







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

//NUEVOOO
app.UseAuthentication();  // Asegúrate de habilitar la autenticación
app.UseAuthorization();

app.MapControllers();
app.Run();

/*
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.8
*/
