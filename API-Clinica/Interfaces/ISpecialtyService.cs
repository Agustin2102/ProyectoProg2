public interface ISpecialtyService
{
    IEnumerable<Specialty> GetAll();
    Specialty? GetById(int id);
    Specialty Create(SpecialtyDTO specialtyDto);
    void Delete(int id);
    Specialty? Update(int id, Specialty specialty);
}
