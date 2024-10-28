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
    public ActionResult<List<Doctor>> GetAllDoctors(){ // Obtiene todos los Doctores de la BD
        return Ok(_doctorService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Doctor> GetById(int id){ //Obtengo un Doctor por su ID
        Doctor? d = _doctorService.GetById(id);

        if(d == null) return NotFound("Doctor not found");
        else return Ok(d);
    }

    [HttpPost]
    public ActionResult<Doctor> Create(DoctorDTO d){
        Doctor _doctor = _doctorService.Create(d); // Llamo al metodo Create del servicio de autor para dar de alta el nuevo Doctor
        return CreatedAtAction(nameof(GetById), new {id = _doctor.Id}, _doctor); // Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo doctor
    }


}