using Microsoft.AspNetCore.Mvc;
using superback.Models;
using superback.Services;

namespace superback.Controllers;

[Route("api/v1/[controller]")]
public class RasgosController: ControllerBase {
    IRasgosService rasgosService;

    public RasgosController(IRasgosService service) {
        rasgosService = service;
    }

    [HttpGet("Habilidades")]
    public IActionResult GetHabilidades() {
        return Ok(rasgosService.GetHabilidades());
    }

    [HttpGet("Debilidades")]
    public IActionResult GetDebilidades() {
        return Ok(rasgosService.GetDebilidades());
    }

    [HttpGet("{id}")]
    public IActionResult GetRasgo(Guid id) {
        return Ok(rasgosService.GetRasgo(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Rasgo rasgo) {
        rasgosService.Save(rasgo);
        if (rasgo.rasgo_id == Guid.Empty) {
            return Conflict();
        } else {
            return Created(new Uri("https://www.google.com/"),rasgo);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Rasgo rasgo) {
        rasgosService.Update(id, rasgo);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        rasgosService.Delete(id);
        return NoContent();
    }    

}