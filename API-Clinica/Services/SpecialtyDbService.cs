using Microsoft.EntityFrameworkCore;

public class SpecialtyDbService : ISpecialtyService{
    private readonly ClinicaContext _context;

    public SpecialtyDbService(ClinicaContext context){
        this._context = context;
    }

    public Specialty Create(SpecialtyDTO a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Specialty> GetAll()
    {
        throw new NotImplementedException();
    }

    public Specialty? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Specialty? Update(int id, Specialty a)
    {
        throw new NotImplementedException();
    }
}