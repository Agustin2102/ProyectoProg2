using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase{
    private readonly IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService){
        this._doctorService = doctorService;
    }

    /*Aqui es donde se tienen que definir los metodos del Services para que se muestren en el navegador*/
 
    [HttpGet]
    [Authorize(Roles = "admin")]
    public ActionResult<List<Doctor>> GetAllDoctors(){ // Obtiene todos los Doctores de la BD
        //Falta mostrar las especialidades de los medicos
        try
      {
        return Ok(_doctorService.GetAll());
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
      }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Doctor> GetById(int id){ //Obtengo un Doctor por su ID
        Doctor? _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found");
        else return Ok(_doctor);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public ActionResult<Doctor> Create(DoctorDTO doctor){
        Doctor _doctor = _doctorService.Create(doctor); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        return CreatedAtAction(nameof(GetById), new {id = _doctor.Id}, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult Delete(int id){
        var _doctor = _doctorService.GetById(id);

        if(_doctor == null) return NotFound("Doctor not found!!!");

        _doctorService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Doctor> UpdateDoctor(int id, DoctorDTO d){
        try{
            Doctor? doctor = _doctorService.Update(id, d);
            if(doctor is null) return NotFound(new {Message = $"No se pudo actualizar el libro con id: {id}"});
            return CreatedAtAction(nameof(GetById), new{id = doctor.Id}, doctor);
        }catch (System.Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }


    [HttpGet("{doctorId}/appointments")]
    [Authorize(Roles = "admin, doctor")]
    public ActionResult<List<Appointment>> GetAllAppointmentsForDoctor(int doctorId){
        try{
            var appointments = _doctorService.GetAllAppointments(doctorId);
            if(!appointments.Any()){
                return NotFound("No appointments found for this doctor.");
            }
            return Ok(appointments);
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }

}