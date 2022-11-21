using Microsoft.AspNetCore.Mvc;
using superback.Models;
using superback.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class PatrocinadoresController: ControllerBase {
    IPatrocinadoresService patrocinadoresService;

    public PatrocinadoresController(IPatrocinadoresService service) {
        patrocinadoresService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(patrocinadoresService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Patrocinador patrocinador) {
        patrocinadoresService.Save(patrocinador);
        if (patrocinador.patrocinador_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),patrocinador);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Patrocinador patrocinador) {
        patrocinadoresService.Update(id, patrocinador);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        patrocinadoresService.Delete(id);
        return NoContent();
    }    

}