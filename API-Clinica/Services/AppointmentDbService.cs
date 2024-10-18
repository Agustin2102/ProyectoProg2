using Microsoft.EntityFrameworkCore;

public class AppointmentDbService : IAppointmentService{
    private readonly ClinicaContext _context;

    public AppointmentDbService(ClinicaContext context){
        this._context = context;
    }

    public Appointment Create(AppointmentDTO a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Appointment> GetAll()
    {
        throw new NotImplementedException();
    }

    public Appointment? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Appointment? Update(int id, Appointment a)
    {
        throw new NotImplementedException();
    }
}