using Microsoft.AspNetCore.Mvc;
using superback.Models;
using superback.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class EventosController: ControllerBase {
    IEventosService eventosService;

    public EventosController(IEventosService service) {
        eventosService = service;
    }

    [HttpGet("agenda/{super_id}")]
    public IActionResult Get(Guid super_id, [FromQuery] string inicio,  [FromQuery] string fin) {
        return Ok(eventosService.GetByUser(super_id,inicio,fin));
    }

    [HttpGet("{id}")]
    public IActionResult GetEvento(Guid id) {
        return Ok(eventosService.Get(id));
    }


    [HttpPost]
    public IActionResult Post([FromBody] Evento evento) {
        eventosService.Save(evento);
        if (evento.evento_id == Guid.Empty) {
            return Conflict();
        } else {
            return Created(new Uri("https://www.google.com/"),evento);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Evento evento) {
        eventosService.Update(id, evento);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        eventosService.Delete(id);
        return NoContent();
    }    

}