public interface IAdministratorService{
    public IEnumerable<Administrator> GetAll();
    public Administrator? GetById(int id);
    public Administrator Create(AdministratorDTO a);

    public void Delete(int id);
    public Administrator? Update(int id, Administrator a);
    public IEnumerable<Appointment> GetAppointment(int id);
}