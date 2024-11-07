public interface IDoctorService{
    public IEnumerable<Doctor> GetAll();
    public Doctor? GetById(int id);
    public Doctor Create(DoctorDTO d);

    public void Delete(int id);
    public Doctor? Update(int id, DoctorDTO d);

    public IEnumerable<Appointment> GetAllAppointments(int doctorID);
    public IEnumerable<Appointment> GetAppointment(int id);


    //hacer que solo el Administrador sea quien cree y elimine los turnos de los medico y los pacientes
    //Es el usuario quien ademas del administrador puede cancelar los turnos


    //ModificarTurnos()?
    //EliminarTurnos()?

}