using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Appointments")]
public class AppointmentController : ControllerBase{
    private readonly IAppointmentService _appointmentService;
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;
    public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService){
        this._appointmentService = appointmentService;
        this._doctorService = doctorService;
        this._patientService = patientService;
    }
}