using Microsoft.AspNetCore.Mvc;
using superback.Models;
using superback.Services;

namespace superback.Controllers;

[Route("api/v1/[controller]")]
public class SupersController: ControllerBase {
    ISupersService supersService;
    SupersContext dbcontext;

    public SupersController(ISupersService service, SupersContext db) {
        supersService = service;
        dbcontext = db;
    }

    //[HttpGet(Name = "GetSuper")]
    //[Route("1")]
    //[Route("2")]
    //[Route("[action]")]
    [HttpGet("Heroes")]
    public IActionResult GetHeroes() {
        return Ok(supersService.GetHeroes());
    }

    [HttpGet("Villanos")]
    public IActionResult GetVillanos() {
        return Ok(supersService.GetVillanos());
    }

    [HttpGet("heroe/{id}")]
    public IActionResult GetHeroe(Guid id) {
        //Console.WriteLine(id);
        return Ok(supersService.GetHeroe(id));
    }

    [HttpGet("heroeByNombre/{nombre}")]
    public IActionResult GetHeroeByNombre(string nombre) {
        return Ok(supersService.GetHeroeByNombre(nombre));
    }

    [HttpGet("villano/{id}")]
    public IActionResult GetVillano(Guid id) {
        return Ok(supersService.GetVillano(id));
    }

    [HttpGet("villanoByNombre/{nombre}")]
    public IActionResult GetVillanoByNombre(string nombre) {
        return Ok(supersService.GetVillanoByNombre(nombre));
    }

    [HttpGet("heroeByHabilidades/{habilidades}")]
    public IActionResult GetHeroeByHabilidades(string habilidades) {
        return Ok(supersService.GetHeroeByHabilidades(habilidades));//GetHeroeByHabilidades
    }

    [HttpGet("worstVillainAgainstTeen")]
    public IActionResult GetWorstVillainAgainstTeen() {
        return Ok(supersService.GetWorstVillainAgainstTeen());
    }
    
    [HttpGet("teenHeroes")]
    public IActionResult GetTeenHeroes() {
        return Ok(supersService.GetTeenHeroes());
    }

    [HttpGet("adultHeroes")]
    public IActionResult GetAdultHeroes() {
        return Ok(supersService.GetAdultHeroes());
    }

    [HttpGet("worstVillainAgainstTeenHero/{id}")]
    public IActionResult GetWorstVillainAgainstTeenHero(Guid id) {
        return Ok(supersService.GetWorstVillainAgainstTeenHero(id));
    }

    [HttpGet("heroeByRelacionesPersonales/{relaciones}")]
    public IActionResult GetHeroeByRelacionesPersonales(string relaciones) {
        return Ok(supersService.GetHeroeByRelacionesPersonales(relaciones));
    }

    
    [HttpGet("villanoByOrigen/{origen}")]
    public IActionResult GetVillanoByOrigen(string origen) {
        return Ok(supersService.GetVillanoByOrigen(origen));
    }

    [HttpGet("villanoByDebilidades/{debilidades}")]
    public IActionResult GetVillanoByDebilidades(string debilidades) {
        return Ok(supersService.GetVillanoByDebilidades(debilidades));//GetHeroeByHabilidades
    }

    [HttpGet("heroeBestSponsor/{id}")]
    public IActionResult GetHeroeBestSponsor(Guid id) {
        return Ok(supersService.GetHeroeBestSponsor(id));
    }

    [HttpGet("top3Heroes")]
    public IActionResult Top3Heroes() {
        return Ok(supersService.Top3Heroes());
    }

    [HttpGet("mostFoughtVillanoByHeroe/{id}")]
    public IActionResult mostFoughtVillanoByHeroe(Guid id) {
        return Ok(supersService.mostFoughtVillanoByHeroe(id));
    }
    
    [HttpGet("patrocinadoresByHeroe")]
    public IActionResult getPatrocinadoresByHeroe() {
        return Ok(supersService.getPatrocinadoresByHeroe());
    }

    [HttpPost("Heroe")]
    public IActionResult PostHeroe([FromBody] Super super) {
        supersService.Save(super,"Heroe");
        if (super.super_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),super);
        }
    }
    [HttpPost("Villano")]
    public IActionResult PostVillano([FromBody] Super super) {
        supersService.Save(super,"Villano");
        if (super.super_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),super);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Super super) {
        supersService.Update(id, super);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        supersService.Delete(id);
        return NoContent();
    }   

    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDatabase()
    {
        dbcontext.Database.EnsureCreated();

        return NoContent();
    }

}