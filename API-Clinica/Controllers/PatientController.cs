using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService){
        this._patientService = patientService;
    }
}