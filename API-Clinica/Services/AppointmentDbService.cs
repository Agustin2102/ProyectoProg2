using Microsoft.EntityFrameworkCore;

public class AppointmentDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public AppointmentDbService(ClinicaContext context){
        this._context = context;
    }
}