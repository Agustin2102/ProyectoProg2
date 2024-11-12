using Microsoft.EntityFrameworkCore;

public class DoctorDbService : IDoctorService{
    private readonly ClinicaContext _context;

    public DoctorDbService(ClinicaContext context){
        this._context = context;
    }
 
    public Doctor Create(DoctorDTO d){
 
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

    public Doctor? GetById(int id){
        return _context.Doctor.Include(s => s.Specialties).FirstOrDefault(d => d.Id == id);
    }

    public Doctor? GetByName(string name)
    {
        return _context.Doctor
        .Include(d => d.Appointments) //Incluyo los turnos del doctor
            .ThenInclude(a => a.Patient) //Tambien a los pacientes de los turnos
        .Include(d => d.Appointments)
            .ThenInclude(a => a.Specialty)
        .Include(d => d.Specialties)
        .FirstOrDefault(d => d.Name == name);
    }

    public int? GetId(string name) {
        return _context.Doctor
            .Where(d => d.Name == name)
            .Select(d => d.Id)
            .FirstOrDefault();
    }


    public Doctor? Update(int id, DoctorDTO d){

        // Buscar el doctor existente en la base de datos
        var doctorUpdate = _context.Doctor
        .Include(doc => doc.Specialties) // Incluir las especialidades para evitar problemas al actualizar la relación
        .FirstOrDefault(doc => doc.Id == id);

        // Actualizar propiedades del doctor existente
        doctorUpdate.Name = d.Name;
        doctorUpdate.LastName = d.LastName;
        doctorUpdate.DNI = d.DNI.Value;
        doctorUpdate.Email = d.Email;
        doctorUpdate.TelephoneNumber = d.TelephoneNumber;
        doctorUpdate.LicenseNumber = d.LicenseNumber.Value;

        // Limpiar las especialidades actuales para luego reasignarlas
        doctorUpdate.Specialties.Clear();

        // Asignar las nuevas especialidades
        if (d.SpecialtyIds != null && d.SpecialtyIds.Any())
        {
            var specialties = _context.Specialty
                .Where(s => d.SpecialtyIds.Contains(s.Id))
                .ToList();
            doctorUpdate.Specialties.AddRange(specialties);
        }


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