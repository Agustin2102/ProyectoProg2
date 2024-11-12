public interface IAdministratorService{
    public IEnumerable<Administrator> GetAll();
    public Administrator? GetById(int id);

    public Administrator? GetByName(string name);
    public int? GetId(string name);
    public Administrator Create(AdministratorDTO a);

    public void Delete(int id);
    public Administrator? Update(int id, AdministratorDTO a);
    public IEnumerable<Appointment> GetAppointment(int id);
}