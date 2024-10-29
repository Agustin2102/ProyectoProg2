using System.Text.Json.Serialization;

public class Specialty
{
    public int Id { get; set; }      // Identificador de la especialidad
    public string Name { get; set; }  // Nombre de la especialidad

    [JsonIgnore]
    public List<Doctor> Doctors{get; set;}
    
    // Constructor    
    public Specialty(int id, string name)
        {
            Id = id;
            Name = name;
        }
    
    public Specialty(){} //constructor sin parametros para poder crear sin tener q pasar el id (specialtydbservice)



    // Override del método ToString
    public override string ToString()
    {
        return $"Specialty ID: {Id}, Name: {Name}";
    }
}
