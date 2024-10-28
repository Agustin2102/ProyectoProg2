using Microsoft.EntityFrameworkCore;

public class AdministratorDbService : IAdministratorService{
    private readonly ClinicaContext _context;

    public AdministratorDbService(ClinicaContext context){
        this._context = context;
    }

    public Administrator Create(AdministratorDTO a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Administrator> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Appointment> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Administrator? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Administrator? Update(int id, Administrator a)
    {
        throw new NotImplementedException();
    }
}