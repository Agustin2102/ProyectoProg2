public interface IAppointmentService{
    public IEnumerable<Appointment> GetAll();
    public Appointment? GetById(int id);
    public Appointment Create(AppointmentDTO a);

    public void Delete(int id);
    public Appointment? Update(int id, Appointment a);
}