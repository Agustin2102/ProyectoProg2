public interface IDoctorService{
    public IEnumerable<Doctor> GetAll();
    public Doctor? GetById(int id);
    public Doctor Create(DoctorDTO d);

    public void Delete(int id);
    public Doctor? Update(int id, DoctorDTO d);
    public IEnumerable<Appointment> GetAppointment(int id);

    //ModificarTurnos()?
    //EliminarTurnos()?

}