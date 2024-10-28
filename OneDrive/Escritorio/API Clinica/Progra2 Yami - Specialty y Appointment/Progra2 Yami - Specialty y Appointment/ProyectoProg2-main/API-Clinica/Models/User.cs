public class User{
    public int Id {get; set;} // ID del usuario
    public string Name {get; set;} // Nombre del usuario
    public string LastName {get; set;} // Apellido del usuario
    public int DNI {get; set;} // DNI del usuario
    public string Email {get; set;} // Correo electrónico del usuario
    public string TelephoneNumber {get; set;} // Número de teléfono del usuario

    // Constructor sin parámetros
    public User(){}

    // Constructor con parámetros
    public User(int id, string name, string lastName, int dni, string email, string telephoneNumber){
        this.Id = id;
        this.Name = name;
        this.LastName = lastName;
        this.DNI = dni;
        this.Email = email;
        this.TelephoneNumber = telephoneNumber;
    }
}
