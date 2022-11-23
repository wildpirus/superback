using superback.Models;
namespace superback.Services;
using Microsoft.EntityFrameworkCore;

public class SupersService : ISupersService {
    
    SupersContext context;

    public SupersService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Super> GetHeroes() {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super).ToList();
    }

    public IEnumerable<Super> GetVillanos() {
        return context.Villanos.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super).ToList();
    }

    public Super GetHeroe(Guid id) {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos)
        .Where(p => p.super_id == id).First().Super;
    }

    public Super GetVillano(Guid id) {
        return context.Villanos.Include(x => x.Super).ThenInclude(s => s.Rasgos)
        .Where(p => p.super_id == id).First().Super;
    }

    public IEnumerable<Super> GetHeroeByNombre(string nombre) {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super)
        .Where(p => p.nombre.Contains(nombre)).ToList();
    }

    public IEnumerable<Super> GetVillanoByNombre(string nombre) {
        return context.Villanos.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super)
        .Where(p => p.nombre.Contains(nombre)).ToList();
    }

    public IEnumerable<Super>  GetHeroeByHabilidades(string habilidades) {
        string[] hab = habilidades.Split(",");
        List<Super> matchHeroes = new List<Super>();
        foreach (Super super in context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super).ToList()) {
            ICollection<Rasgo> rasgosSuper = super.Rasgos;
            HashSet<string> s = new HashSet<string>();
            foreach (Rasgo rasgo in rasgosSuper) {
                if (rasgo.tipo_rasgo == "Habilidad") {
                    s.Add(rasgo.titulo);
                }
            }
            int p = s.Count;
            foreach (string habilidad in hab){
                s.Add(habilidad);
            }
            if (s.Count == p) {
                matchHeroes.Add(super);
            }
        }
        return matchHeroes;
    }

    public Object GetWorstVillainAgainstTeen() {
        var query = (from lucha in context.Luchas
        join villano in context.Villanos
        on lucha.villano_id equals villano.villano_id
        /*join superVillano in context.Supers
        on villano.super_id equals superVillano.super_id*/
        join heroe in context.Heroes
        on lucha.heroe_id equals heroe.heroe_id
        join superHeroe in context.Supers
        on heroe.super_id equals superHeroe.super_id
        where (
            superHeroe.edad > 9 && 
            superHeroe.edad < 20 && 
            lucha.ganaHeroe == true 
        )select new { 
            Villano = villano.super_id,
            Heroe = heroe.super_id,
            Fight = lucha.lucha_id
        }).GroupBy(x => new {x.Villano, x.Heroe} ).Select(x => new {
            Supers = x.Key,
            Peleas = x.Count()
        }).OrderByDescending(x => x.Peleas).First();

        return new {
            Villano = context.Supers.Where(v => v.super_id == query.Supers.Villano).First(),
            Heroe = context.Supers.Where(v => v.super_id == query.Supers.Heroe).First(),
            Peleas = query.Peleas
        };
    }

    public IEnumerable<Super> GetTeenHeroes () {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Where(x => x.Super.edad > 9 && x.Super.edad < 20).Select(s=>s.Super).ToList();
    }

    public IEnumerable<Super> GetAdultHeroes () {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Where(x => x.Super.edad > 19).Select(s => s.Super).ToList();
    }

    public Object GetWorstVillainAgainstTeenHero(Guid id) {
        var query = (from heroe in context.Heroes
        join superHeroe in context.Supers
        on heroe.super_id equals superHeroe.super_id
        join lucha in context.Luchas
        on heroe.heroe_id equals lucha.heroe_id
        join villano in context.Villanos
        on lucha.villano_id equals villano.villano_id
        where (
            superHeroe.super_id == id && 
            superHeroe.edad > 9 && 
            superHeroe.edad < 20 &&
            lucha.ganaHeroe 
        )select new { 
            Villano = villano.super_id,
            Fight = lucha.lucha_id
        }).GroupBy(x => x.Villano ).Select(x => new {
            Villano = x.Key,
            Peleas = x.Count()
        }).OrderByDescending(x => x.Peleas).First();

        return new {
            Villano = context.Supers.Where(v => v.super_id == query.Villano).First(),
            Peleas = query.Peleas
        };
    }

    public IEnumerable<Super> GetHeroeByRelacionesPersonales(string relaciones){
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos)
            .Where(x => x.Super.relaciones.Contains(relaciones)).Select(s=>s.Super).ToList();
    }

    public IEnumerable<Super> GetVillanoByOrigen(string origen){
        return context.Villanos.Include(x => x.Super).ThenInclude(s => s.Rasgos)
            .Where(x => x.Super.origen.Contains(origen.ToLowerInvariant())).Select(s=>s.Super).ToList();
    }

    public IEnumerable<Super> GetVillanoByDebilidades(string debilidades) {
        string[] deb = debilidades.Split(",");
        List<Super> matchVillanos = new List<Super>();
        foreach (Super super in context.Villanos.Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=>s.Super).ToList()) {
            ICollection<Rasgo> rasgosSuper = super.Rasgos;
            HashSet<string> s = new HashSet<string>();
            foreach (Rasgo rasgo in rasgosSuper) {
                if (rasgo.tipo_rasgo == "Debilidad") {
                    s.Add(rasgo.titulo);
                }
            }
            int p = s.Count;
            foreach (string debilidad in deb){
                s.Add(debilidad);
            }
            if (s.Count == p) {
                matchVillanos.Add(super);
            }
        }
        return matchVillanos;
    }

    public Patrocinador GetHeroeBestSponsor(Guid id) {
        return context.Heroes.Include(x => x.Super).ThenInclude(s => s.Rasgos).Where(x => x.super_id == id).First()
        .Patrocinadores.OrderByDescending(p => p.monto).First();
    }

    public IEnumerable<Object> Top3Heroes(){
        var query = (from lucha in context.Luchas
        join heroe in context.Heroes
        on lucha.heroe_id equals heroe.heroe_id
        where lucha.ganaHeroe
        group lucha by lucha.heroe_id into g
        select  new {
            Heroe = g.Key,
            Count = g.Count()
        }).OrderByDescending(p => p.Count).Take(3).ToList();

        List<Object> best3Heroes = new List<Object>();
        foreach (var item in query){
            //Console.WriteLine("Heroe {0}: {1}", item.Heroe, item.Count);
            //best3Heroes.Add(context.Supers.Include(x => x.Luchas).Where(x => x.super_id == item.Heroe).First());
            Heroe heroe = context.Heroes.Include(x => x.Luchas).Include(x => x.Super).ThenInclude(s => s.Rasgos).Where(x => x.heroe_id == item.Heroe).First();
            best3Heroes.Add(new {
                super_id = heroe.super_id,
                nombre = heroe.Super.nombre,
                edad = heroe.Super.edad,
                relaciones = heroe.Super.relaciones,
                origen = heroe.Super.origen,
                Rasgos = heroe.Super.Rasgos,
                Luchas = heroe.Luchas
            });
        }

        
        return best3Heroes;
    }

    public Object mostFoughtVillanoByHeroe(Guid id) {
        var query = (from heroe in context.Heroes
        join lucha in context.Luchas
        on heroe.heroe_id equals lucha.heroe_id
        join villano in context.Villanos
        on lucha.villano_id equals villano.villano_id
        where (
            heroe.super_id == id
        )group villano by villano.super_id into p
        select new {
            Villano = p.Key,
            Peleas = p.Count()
        }).OrderByDescending(x => x.Peleas).First();

        //Console.WriteLine("Villano {0}, peleas: {1}",query.Villano,query.Peleas);
        var atx = (from heroe in context.Heroes
        join lucha in context.Luchas
        on heroe.heroe_id equals lucha.heroe_id
        join villano in context.Villanos
        on lucha.villano_id equals villano.villano_id
        where (
            heroe.super_id == id &&
            villano.super_id == query.Villano
        )select lucha)
        .Include(e => e.Heroe).ThenInclude(x => x.Super)
        .Include(e => e.Villano).ThenInclude(x => x.Super)
        .ToList();
        return new {
            Villano = context.Supers.Where(s => s.super_id == query.Villano).First(),
            Numero = query.Peleas,
            Peleas = atx
        };
    }

    public IEnumerable<Object> getPatrocinadoresByHeroe() {
        return context.Heroes.Include(x=> x.Patrocinadores).Include(x => x.Super).ThenInclude(s => s.Rasgos).Select(s=> new {
            super_id = s.super_id,
            nombre = s.Super.nombre,
            edad = s.Super.edad,
            relaciones = s.Super.relaciones,
            origen = s.Super.origen,
            Rasgos = s.Super.Rasgos,
            Patrocinadores = s.Patrocinadores 
        }).ToList();
    }           

    public async Task Save(Super super, string tipo) {
        int exist = context.Supers.Where(s => s.nombre == super.nombre).Count();
        if (exist == 0) {
            super.super_id = Guid.NewGuid();
            super.nombre = super.nombre.ToLowerInvariant();
            super.img = super.img;
            super.relaciones = super.relaciones.ToLowerInvariant();
            super.origen = super.origen.ToLowerInvariant();
            await context.AddAsync(super);
            await context.SaveChangesAsync();
            if(tipo == "Heroe"){
                Heroe heroe = new Heroe(){
                    heroe_id = Guid.NewGuid(),
                    super_id = super.super_id
                };
                await context.AddAsync(heroe);
                await context.SaveChangesAsync();
            }else {
                Villano villano = new Villano(){
                    villano_id = Guid.NewGuid(),
                    super_id = super.super_id
                };
                await context.AddAsync(villano);
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task Update(Guid id, Super super) {
        var superActual = context.Supers.Find(id);

        if(superActual != null) {
            superActual.nombre = super.nombre.ToLowerInvariant();
            superActual.relaciones = super.relaciones.ToLowerInvariant();
            superActual.origen = super.origen.ToLowerInvariant();

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var superActual = context.Supers.Find(id);

        if(superActual != null) {
            context.Remove(superActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface ISupersService {

    IEnumerable<Super> GetHeroes();
    IEnumerable<Super> GetVillanos();
    Super GetHeroe(Guid id);
    Super GetVillano(Guid id);
    IEnumerable<Super> GetHeroeByNombre(string nombre);
    IEnumerable<Super> GetVillanoByNombre(string nombre);
    IEnumerable<Super> GetHeroeByHabilidades(string habilidades);
    Object GetWorstVillainAgainstTeen();
    IEnumerable<Super> GetTeenHeroes();
    IEnumerable<Super> GetAdultHeroes();
    Object GetWorstVillainAgainstTeenHero(Guid id);
    IEnumerable<Super> GetHeroeByRelacionesPersonales(string relaciones);
    IEnumerable<Super> GetVillanoByOrigen(string origen);
    IEnumerable<Super>  GetVillanoByDebilidades(string debilidades);
    Patrocinador GetHeroeBestSponsor(Guid id);
    IEnumerable<Object> Top3Heroes();
    Object mostFoughtVillanoByHeroe(Guid id);
    IEnumerable<Object> getPatrocinadoresByHeroe();
    Task Save(Super super, string tipo);
    Task Update(Guid id, Super super);
    Task Delete(Guid id);
}