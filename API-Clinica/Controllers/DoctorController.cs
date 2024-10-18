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
    public ActionResult<List<Doctor>> GetAllDoctors(){
        return Ok(_doctorService.GetAll());
    }


}