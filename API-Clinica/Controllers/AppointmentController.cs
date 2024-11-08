using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    //constructor que recibe la dependencia del servicio de citas
    public AppointmentController(IAppointmentService appointmentService) 
    {
        this._appointmentService = appointmentService; // inicializa el servicio de citas
    }


    [HttpGet] 
     [Authorize(Roles = "admin")]
    public ActionResult<List<Appointment>> GetAllAppointments() 
    {
        var appointments = _appointmentService.GetAll(); // obtiene la lista de citas
        return Ok(appointments); // retorna la lista de citas
    }


    [HttpGet("{id}")] 
     [Authorize(Roles = "admin")]
    public ActionResult<Appointment> GetById(int id) 
    {
        var appointment = _appointmentService.GetById(id); // busca la cita por id
        if (appointment == null) // verifica si no se encontro la cita
        {
            return NotFound("Appointment not found"); // retorna 404 si no se encuentra
        }
        return Ok(appointment); // retorna la cita encontrada
    }

  
    [HttpPost] 
     [Authorize(Roles = "admin, patient")]
    public ActionResult<Appointment> Create([FromBody] AppointmentDTO appointmentDto) 
    {
        if (appointmentDto == null) 
    {
        return BadRequest("Appointment data is required.");
    }

    if (!ModelState.IsValid) 
    {
        return BadRequest(ModelState);
    }

    try
    {
        // Llamar al servicio para crear la cita, que ya incluye la validación de superposición
        Appointment _appointment = _appointmentService.Create(appointmentDto);

        if (_appointment == null) 
        {
            return BadRequest("Error creating appointment.");
        }

        return CreatedAtAction(nameof(GetById), new { id = _appointment.ID }, _appointment); 
    }
    catch (InvalidOperationException ex)
    {
        // Si se lanza una excepción por superposición de turnos, retornar conflicto
        return Conflict(ex.Message); // El mensaje de la excepción es el que generaste en el servicio
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An error occurred while creating the appointment.", error = ex.Message });
    }
    }

    // metodo para actualizar una cita existente
    [HttpPut("{id}")] 
     [Authorize(Roles = "admin")]
    public IActionResult Update(int id, [FromBody] AppointmentDTO appointmentDto) 
    {
        if (appointmentDto == null) // comprueba si el DTO es nulo
        {
            return BadRequest("Appointment data is required."); // retorna 400 si es nulo
        }

        // validar que los campos requeridos estan presentes
        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState); // retorna 400 si hay errores en el modelo
        }

        var updatedAppointment = _appointmentService.Update(id, appointmentDto); // actualiza la cita
        if (updatedAppointment == null) // verifica si la cita no se encontro
        {
            return NotFound("Appointment not found"); // retorna 404 si no se encuentra
        }
        return Ok(updatedAppointment); // retorna la cita actualizada
    }

    // metodo para eliminar una cita por su ID
    [HttpDelete("{id}")] 
     [Authorize(Roles = "admin")]
    public IActionResult DeleteAppointment(int id) 
    {
        var appointment = _appointmentService.GetById(id); // busca la cita por id
        if (appointment == null) // verifica si no se encontro la cita
        {
            return NotFound(); // devuelve 404 si no se encuentra
        }

        _appointmentService.Delete(id); // elimina la cita

        return Ok(new { message = "El turno se eliminó correctamente." }); // retorna 200 con mensaje de exito
    }
}