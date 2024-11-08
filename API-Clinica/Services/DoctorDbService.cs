using Microsoft.EntityFrameworkCore;

public class DoctorDbService : IDoctorService{
    private readonly ClinicaContext _context;

    public DoctorDbService(ClinicaContext context){
        this._context = context;
    }

    public Doctor Create(DoctorDTO d){

        /* if (d.DNI == null){ // Manejar el caso en el que DNI sea nulo
            throw new ArgumentException("DNI cannot be null");
        } */
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

    /* public IEnumerable<Appointment> GetAllAppointments(int doctorID){
        return _context.Appointment
        .Include(a => a.Doctor) //Incluyo las propiedades de navegacion, lo que me va a permitir modificar la informacion que se envia del turno el en controlador
        .Include(a => a.Patient)
        .Include(a => a.Specialty)
        .Where(d => d.doctor_id == doctorID).ToList();
    } */


    /* public Appointment GetAppointment(int doctorID, int appointmentID){
        return _context.Appointment
        .Include(a => a.Doctor) //Incluyo las propiedades de navegacion, lo que me va a permitir modificar la informacion que se envia del turno el en controlador
        .Include(a => a.Patient)
        .Include(a => a.Specialty)
        .FirstOrDefault(d => d.doctor_id == doctorID && d.ID == appointmentID); //Envio el primer turno que comple con esas condifiocnes
    } */

    public Doctor? GetById(int id){
        return _context.Doctor.Find(id);
    }

    public Doctor? GetByName(string name)
    {
        return _context.Doctor
        .Include(d => d.Appointments) //Incluyo los turnos del doctor
            .ThenInclude(a => a.Patient) //Tambien a los pacientes de los turnos
        .Include(d => d.Appointments)
            .ThenInclude(a => a.Specialty)
        .FirstOrDefault(d => d.Name == name);
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


/*

public IEnumerable<Appointment> GetAllAppointments() {
    int doctorID = int.Parse(User.FindFirst("DoctorID").Value);
    return _context.Appointment.Where(d => d.doctor_id == doctorID).ToList();
}

public Appointment GetAppointment(int appointmentID) {
    int doctorID = int.Parse(User.FindFirst("DoctorID").Value);
    return _context.Appointment.FirstOrDefault(d => d.doctor_id == doctorID && d.ID == appointmentID);
}

Aquí, User.FindFirst("DoctorID").Value obtiene el ID del usuario autenticado, evitando que tengas que pasarlo como parámetro. Esto mejora la seguridad al limitar el acceso solo a los recursos del doctor autenticado.



*/