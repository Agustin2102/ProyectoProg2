using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/administrators")]
public class AdministratorController : ControllerBase{

    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService){
        this._administratorService = administratorService;
    }
}