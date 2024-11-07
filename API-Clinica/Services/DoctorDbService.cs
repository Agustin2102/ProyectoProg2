using Microsoft.EntityFrameworkCore;

public class DoctorDbService : IDoctorService{
    private readonly ClinicaContext _context;

    public DoctorDbService(ClinicaContext context){
        this._context = context;
    }

    public Doctor Create(DoctorDTO d){

        if (d.DNI == null){ // Manejar el caso en el que DNI sea nulo
            throw new ArgumentException("DNI cannot be null");
        }
        if (d.LicenseNumber == null){ // Manejar el caso en el que LicenseNumber sea nulo
            throw new ArgumentException("LicenseNumber cannot be null");
        }
 
        Doctor doctor = new(){
            Name = d.Name,
            LastName = d.LastName,
            DNI = d.DNI.Value,
            Email = d.Email,
            TelephoneNumber = d.TelephoneNumber,
            LicenseNumber = d.LicenseNumber.Value,
            Specialties = new List<Specialty>() //Inicializo la lista de especialidades
        };

        //Asocio las especialidades
        if(d.SpecialtyIds != null && d.SpecialtyIds.Any()){
            //Obtengo las especialidades del contexto
            var specialties = _context.Specialty.Where(s => d.SpecialtyIds.Contains(s.Id)).ToList();
            //Añado las especialidades al doctor
            doctor.Specialties.AddRange(specialties);
        }

        _context.Doctor.Add(doctor);
        _context.SaveChanges();
        return doctor;
    }
 
    public void Delete(int id){
        var a = _context.Doctor.Find(id);
        _context.Doctor.Remove(a);
        _context.SaveChanges();
    }

    public IEnumerable<Doctor> GetAll(){
        return _context.Doctor.Include(s => s.Specialties);
    }

    public IEnumerable<Appointment> GetAllAppointments(int doctorID){
        return _context.Appointment.Where(d => d.doctor_id == doctorID).ToList();
    }


    public IEnumerable<Appointment> GetAppointment(int id){
        throw new NotImplementedException();
    }

    public Doctor? GetById(int id){
        return _context.Doctor.Find(id);
    }

    public Doctor? Update(int id, DoctorDTO d){
        Doctor doctorUpdate = new() {
            Id = id,
            Name = d.Name,
            LastName = d.LastName,
            DNI = d.DNI.Value,
            Email = d.Email,
            TelephoneNumber = d.TelephoneNumber,
            LicenseNumber = d.LicenseNumber.Value
        };
        _context.Entry(doctorUpdate).State = EntityState.Modified;
        _context.SaveChanges();
        return doctorUpdate;

    }
}