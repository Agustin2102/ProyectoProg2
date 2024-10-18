public class Specialty
{
    public int Id { get; set; }      // Identificador de la especialidad
    public string Name { get; set; }  // Nombre de la especialidad

    // Constructor
    public Specialty(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    // Override del método ToString
    public override string ToString()
    {
        return $"Specialty ID: {Id}, Name: {Name}";
    }
}
