using Microsoft.EntityFrameworkCore;

public class PatientDbService : IPatientService{
    private readonly ClinicaContext _context;

    public PatientDbService(ClinicaContext context){
        this._context = context;
    }

    public Patient Create(PatientDTO a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Patient> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Appointment> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    public Patient? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Patient? Update(int id, Patient a)
    {
        throw new NotImplementedException();
    }
}