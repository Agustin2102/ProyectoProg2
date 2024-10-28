using Microsoft.EntityFrameworkCore;

public class AppointmentDbService : IAppointmentService
{
    private readonly ClinicaContext _context;

    public AppointmentDbService(ClinicaContext context)
    {
        this._context = context;
    }

    //obtener todas las citas
    public IEnumerable<Appointment> GetAll()
    {
        return _context.Appointment
                       .Include(a => a.Patient)   //incluir Paciente
                       .Include(a => a.Doctor)    //incluir Doctor
                       .Include(a => a.Specialty)  //incluir Especialidad si es necesario
                       .ToList();
    }

    //obtener una cita por ID
    public Appointment? GetById(int id)
    {
        return _context.Appointment
                       .Include(a => a.Patient)   //incluir Paciente
                       .Include(a => a.Doctor)    //incluir Doctor
                       .Include(a => a.Specialty)  //incluir Especialidad si es necesario
                       .FirstOrDefault(a => a.ID == id);
    }

    //crear una nueva cita --- convierte el AppointmentDTO en una entidad Appointment y la guarda en la base de datos
    public Appointment Create(AppointmentDTO appointmentDto)
    {
        var appointment = new Appointment
        {
            patient_id = appointmentDto.patient_id,
            doctor_id = appointmentDto.doctor_id,        
            specialty_id = appointmentDto.specialty_id,  
            appointment_date = appointmentDto.appointment_date, 
            status = appointmentDto.status                 
        };

        _context.Appointment.Add(appointment);
        _context.SaveChanges();

        return appointment;
    }

    //eliminar una cita por ID
    public void Delete(int id)
    {
        var appointment = GetById(id); //busca la cita por ID
        if (appointment != null)
        {
            _context.Appointment.Remove(appointment); // elimina la cita
            _context.SaveChanges(); // guarda los cambios en la base de datos
        }
    }

    // actualizar una cita
    public Appointment? Update(int id, AppointmentDTO appointmentDto)
    {
        var appointment = GetById(id);
        if (appointment != null)
        {
            appointment.patient_id = appointmentDto.patient_id ;     
            appointment.doctor_id = appointmentDto.doctor_id ;        
            appointment.specialty_id = appointmentDto.specialty_id;  
            appointment.appointment_date = appointmentDto.appointment_date; 
            appointment.status = appointmentDto.status ;              

            _context.SaveChanges();
            return appointment;
        }

        return null;
    }
}