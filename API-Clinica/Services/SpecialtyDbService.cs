using Microsoft.EntityFrameworkCore;

public class SpecialtyDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public SpecialtyDbService(ClinicaContext context){
        this._context = context;
    }
}