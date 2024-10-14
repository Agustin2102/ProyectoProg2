using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/specialties")]
public class SpecialtyController : ControllerBase{
    private readonly ISpecialtyService _specialtyService;
    private readonly IDoctorService _doctorService;

    public SpecialtyController(ISpecialtyService specialtyService, IDoctorService doctorService){
        this._specialtyService = specialtyService;
        this._doctorService = doctorService;
    }

    public SpecialtyController(ISpecialtyService specialtyService){
        this._specialtyService = specialtyService;
    }
}