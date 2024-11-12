public interface IPatientService{
    public IEnumerable<Patient> GetAll();
    public Patient? GetById(int id);

    public Patient? GetByName(string name);
    public int? GetId(string name);
    
    public Patient Create(PatientDTO a);

    public void Delete(int id);
    public Patient? Update(int id, PatientDTO a);

    //public IEnumerable<Appointment> GetAllAppointments(int PatientId);
    //public IEnumerable<Appointment> GetAppointment(int id);

} 