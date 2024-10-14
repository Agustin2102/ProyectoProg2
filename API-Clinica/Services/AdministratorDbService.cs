using Microsoft.EntityFrameworkCore;

public class AdministratorDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public AdministratorDbService(ClinicaContext context){
        this._context = context;
    }
}