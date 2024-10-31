using Microsoft.EntityFrameworkCore;

public class AdministratorDbService : IAdministratorService
{
    private readonly ClinicaContext _context;

    public AdministratorDbService(ClinicaContext context)
    {
        this._context = context;
    }

    public Administrator Create(AdministratorDTO a)
    {

        Administrator administrator = new()
        {
            Name = a.Name,
            LastName = a.LastName,
            DNI = a.DNI.Value,
            Email = a.Email,
            TelephoneNumber = a.TelephoneNumber,

        };
        _context.Administrator.Add(administrator);
        _context.SaveChanges();
        return administrator;
    }


    public void Delete(int id)
    {
        var a = _context.Administrator.Find(id);
        _context.Administrator.Remove(a);
        _context.SaveChanges();
    }

    public IEnumerable<Administrator> GetAll()
    {
        return _context.Administrator;
    }

    public IEnumerable<Appointment> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Administrator? GetById(int id)
    {
        return _context.Administrator.Find(id);
    }

    public Administrator? Update(int id, Administrator a)
    {
        _context.Entry(a).State = EntityState.Modified;
        _context.SaveChanges();
        return a;
    }
}