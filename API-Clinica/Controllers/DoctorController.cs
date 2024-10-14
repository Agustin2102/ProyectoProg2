using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase{
    private readonly IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService){
        this._doctorService = doctorService;
    }
}