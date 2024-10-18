public interface ISpecialtyService{
    public IEnumerable<Specialty> GetAll();
    public Specialty? GetById(int id);
    public Specialty Create(SpecialtyDTO a);

    public void Delete(int id);
    public Specialty? Update(int id, Specialty a);

}