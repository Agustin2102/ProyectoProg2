using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class AccountDbService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountDbService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserName(){
        //Accede al contexto HTTP
        var user = _httpContextAccessor.HttpContext?.User;

        //Obtengo el nombre del usuario del claim "name"
        return user?.Identity?.Name;
    }

    /* public object GetUserClaims()
    {
        // Accede al contexto HTTP
        var user = _httpContextAccessor.HttpContext?.User;

        // Obtiene el nombre de usuario (si está presente)
        var userName = user?.Identity?.Name;

        // Obtiene todos los claims del usuario autenticado
        var claims = user?.Claims.Select(c => new 
        {
            Type = c.Type,
            Value = c.Value
        });

        return new { userName, claims };
    } */

}

/*
    xplicación de los cambios:
Inyección de IHttpContextAccessor:

Ahora estamos inyectando IHttpContextAccessor en el constructor del servicio AccountDbService. Esto nos permite acceder al contexto HTTP, que es donde se encuentran los claims del usuario autenticado.
Acceso a HttpContext:

Usamos _httpContextAccessor.HttpContext?.User para acceder al contexto HTTP y obtener los claims del usuario autenticado. Este es el método adecuado cuando no estás en un controlador, sino en un servicio.
Eliminación de la herencia de ControllerBase:

Ya que este es un servicio y no un controlador, no es necesario que herede de ControllerBase. Esto también significa que ya no puedes usar métodos como Ok() o Problem() directamente. Si necesitas enviar una respuesta HTTP en algún punto, será el controlador quien maneje esas respuestas, no el servicio.
*/