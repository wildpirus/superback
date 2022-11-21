using Microsoft.AspNetCore.Mvc;
using superback.Models;
using superback.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class LuchasController: ControllerBase {
    ILuchasService luchasService;

    public LuchasController(ILuchasService service) {
        luchasService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(luchasService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Lucha lucha) {
        luchasService.Save(lucha);
        if (lucha.lucha_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),lucha);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Lucha lucha) {
        luchasService.Update(id, lucha);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        luchasService.Delete(id);
        return NoContent();
    }    

}