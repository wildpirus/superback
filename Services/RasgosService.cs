using superback.Models;
namespace superback.Services;
using Microsoft.EntityFrameworkCore;

public class RasgosService : IRasgosService{
    
    SupersContext context;

    public RasgosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<string> GetHabilidades(){
        return context.Rasgos.Where(h => h.tipo_rasgo == "Habilidad").Select(h => h.titulo).Distinct();
    }
    public IEnumerable<string> GetDebilidades(){
        return context.Rasgos.Where(h => h.tipo_rasgo == "Debilidad").Select(h => h.titulo).Distinct();;
    }
    public Rasgo GetRasgo(Guid id){
        return context.Rasgos.Where(h => h.rasgo_id == id).First();
    }

    public async Task Save(Rasgo rasgo) {
        int exist = context.Rasgos.Where(r => r.titulo == rasgo.titulo && r.tipo_rasgo == rasgo.tipo_rasgo && r.super_id == rasgo.super_id).Count();
        if (exist == 0) {
            rasgo.rasgo_id = Guid.NewGuid();
            rasgo.titulo = rasgo.titulo.ToLowerInvariant();
            await context.AddAsync(rasgo);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Guid id, Rasgo rasgo) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            rasgoActual.titulo = rasgo.titulo;
            rasgoActual.tipo_rasgo = rasgo.tipo_rasgo;
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            context.Remove(rasgoActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IRasgosService {
    IEnumerable<string> GetHabilidades();
    IEnumerable<string> GetDebilidades();
    Rasgo GetRasgo(Guid id);
    Task Save(Rasgo super);

    Task Update(Guid id, Rasgo super);

    Task Delete(Guid id);
}