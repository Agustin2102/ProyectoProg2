using Microsoft.EntityFrameworkCore;

public class PatientDbService : IPatientService
{
    private readonly ClinicaContext _context;

    public PatientDbService(ClinicaContext context)
    {
        this._context = context;
    }

    public Patient Create(PatientDTO d)
    {
        Patient patient = new()
        {
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

    /*      public IEnumerable<Appointment> GetAllAppointments(int PatientId){
         return _context.Appointment.Where(d => d.patient_id == PatientId).ToList();
     } */


    /* public IEnumerable<Appointment> GetAppointment(int id){
        throw new NotImplementedException();
    } */

    public void Delete(int id){

        var patient = _context.Patient.Include(p => p.Appointments).FirstOrDefault(p => p.Id == id);

        if (patient != null){
            // Elimina todas os turnos asociadas a este paciente
            _context.Appointment.RemoveRange(patient.Appointments);

            // Luego elimina el paciente
            _context.Patient.Remove(patient);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Patient> GetAll(){
        return _context.Patient;
    }



    public Patient? GetById(int id){
        return _context.Patient.Find(id);
    }

    public Patient? GetByName(string name){

        return (Patient?)_context.Patient
            .Include(p => p.Appointments) //Incluyo los turnos del Paciente
                .ThenInclude(a => a.Doctor) // Incluyo al Doctor del turno para acceder a los datos
            .Include(p => p.Appointments)
                .ThenInclude(a => a.Specialty)// Incluto la especialidad asociada al turno para acceder a los datos
            .FirstOrDefault(d => d.Name == name);
    }

    public int? GetId(string name)
    {
        return _context.Patient
            .Where(p => p.Name == name)
            .Select(p => p.Id)
            .FirstOrDefault();
    }

    public Patient? Update(int id, PatientDTO patientDTO){

        //Busco el paciente en la base de datos
        var patientUpdate = GetById(id);

        // Asignar valores desde el DTO al paciente
        patientUpdate.Name = patientDTO.Name;
        patientUpdate.LastName = patientDTO.LastName;
        patientUpdate.DNI = patientDTO.DNI;
        patientUpdate.Email = patientDTO.Email;
        patientUpdate.TelephoneNumber = patientDTO.TelephoneNumber;
        patientUpdate.DateOfBirth = patientDTO.DateOfBirth;
        patientUpdate.Address = patientDTO.Address;
        patientUpdate.MedicalHistory = patientDTO.MedicalHistory;
        

        _context.Entry(patientUpdate).State = EntityState.Modified;
        _context.SaveChanges();
        return patientUpdate;
    }

}