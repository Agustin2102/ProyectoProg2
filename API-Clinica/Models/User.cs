public class User{
    protected int Id {get; set;} // ID del usuario
    protected string Name {get; set;} // Nombre del usuario
    protected string LastName {get; set;} // Apellido del usuario
    protected int DNI {get; set;} // DNI del usuario
    protected string Email {get; set;} // Correo electrónico del usuario
    protected string TelephoneNumber {get; set;} // Número de teléfono del usuario

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
