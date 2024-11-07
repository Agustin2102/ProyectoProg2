using Microsoft.EntityFrameworkCore;

public class PatientDbService : IPatientService{
    private readonly ClinicaContext _context;

    public PatientDbService(ClinicaContext context){
        this._context = context;
    }

    public Patient Create(PatientDTO d)
    {
    Patient patient = new(){
            Name = d.Name,
            LastName = d.LastName,
            DNI = d.DNI,
            Email = d.Email,
            TelephoneNumber = d.TelephoneNumber,
            DateOfBirth = d.DateOfBirth,
            Address = d.Address,
             MedicalHistory = d.MedicalHistory ?? "" // Valor predeterminado si es nulo

        };
        _context.Patient.Add(patient);
        _context.SaveChanges();
        return patient;
    }

        public IEnumerable<Appointment> GetAllAppointments(int PatientId){
        return _context.Appointment.Where(d => d.patient_id == PatientId).ToList();
    }


    public IEnumerable<Appointment> GetAppointment(int id){
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
         var patient = _context.Patient.Include(p => p.Appointments).FirstOrDefault(p => p.Id == id);
    
    if (patient != null)
    {
        // Elimina todas os turnos asociadas a este paciente
        _context.Appointment.RemoveRange(patient.Appointments);

        // Luego elimina el paciente
        _context.Patient.Remove(patient);
        _context.SaveChanges();
    }
    }

    public IEnumerable<Patient> GetAll()
    {
        return _context.Patient;
    }

  

    public Patient? GetById(int id)
    {
        return _context.Patient.Find(id);
    }

    public Patient? Update(int id, Patient a)
    {
        _context.Entry(a).State = EntityState.Modified;
        _context.SaveChanges();
        return a;
    }
}