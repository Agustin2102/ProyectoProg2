public interface IDoctorService{
    public IEnumerable<Doctor> GetAll();
    public Doctor? GetById(int id);
    public Doctor Create(DoctorDTO a);

    public void Delete(int id);
    public Doctor? Update(int id, Doctor a);
    public IEnumerable<Appointment> GetAppointment(int id);

    //ModificarTurnos()?
    //EliminarTurnos()?

}