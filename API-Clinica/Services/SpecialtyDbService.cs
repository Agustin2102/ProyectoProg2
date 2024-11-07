//using Microsoft.EntityFrameworkCore;

public class SpecialtyDbService : ISpecialtyService
{
    private readonly ClinicaContext _context;

    public SpecialtyDbService(ClinicaContext context)
    {
        this._context = context;
    }

    //obtener todas las especialidades
    public IEnumerable<Specialty> GetAll()
    {
        return _context.Specialty.ToList();
    }

    //obtener una especialidad por ID
    public Specialty? GetById(int id)
    {
        return _context.Specialty.Find(id);
    }

    //crear una nueva especialidad
    public Specialty Create(SpecialtyDTO specialtyDto)
    {
        var specialty = new Specialty
        {
            Name = specialtyDto.Name
        };

        _context.Specialty.Add(specialty);
        _context.SaveChanges();

        return specialty;
    }


    //eliminar una especialidad por ID
    public void Delete(int id)
    {
        var specialty = _context.Specialty.Find(id);
        if (specialty != null)
        {
            _context.Specialty.Remove(specialty);
            _context.SaveChanges();
        }
    }

    //actualizar una especialidad existente
    public Specialty? Update(int id, Specialty updatedSpecialty)
    {
        var specialty = _context.Specialty.Find(id);
        if (specialty == null)
            return null;

        specialty.Name = updatedSpecialty.Name;
        _context.SaveChanges();

        return specialty;
    }
}
