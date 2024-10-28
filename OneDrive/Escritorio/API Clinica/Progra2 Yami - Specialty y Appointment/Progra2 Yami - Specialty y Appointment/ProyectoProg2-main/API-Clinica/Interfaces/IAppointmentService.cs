public interface IAppointmentService
{
    IEnumerable<Appointment> GetAll(); //pbtiene todos los turnos
    Appointment? GetById(int id); //obtiene un turno por ID, puede ser nulo si no se encuentra
    Appointment Create(AppointmentDTO appointment); //crea un nuevo usando un DTO

    void Delete(int id); //elimina un tunrno por ID
    Appointment? Update(int id, AppointmentDTO appointmentDto); //actualiza un truno usando un DTO, devuelve null si no existe
}
