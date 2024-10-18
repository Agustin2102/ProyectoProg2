using Microsoft.EntityFrameworkCore;

public class DoctorDbService : IDoctorService{
    private readonly ClinicaContext _context;

    public DoctorDbService(ClinicaContext context){
        this._context = context;
    }

    public Doctor Create(DoctorDTO a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Doctor> GetAll()
    {
        return _context.Doctor;
    }

    public IEnumerable<Appointment> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Doctor? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Doctor? Update(int id, Doctor a)
    {
        throw new NotImplementedException();
    }
}