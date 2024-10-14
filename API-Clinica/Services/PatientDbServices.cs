using Microsoft.EntityFrameworkCore;

public class PatientDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public PatientDbService(ClinicaContext context){
        this._context = context;
    }
}