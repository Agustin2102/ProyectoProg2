using Microsoft.EntityFrameworkCore;

public class DoctorDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public DoctorDbService(ClinicaContext context){
        this._context = context;
    }
}