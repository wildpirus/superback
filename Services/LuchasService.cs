using superback.Models;
namespace superback.Services;
using Microsoft.EntityFrameworkCore;

public class LuchasService : ILuchasService {
    
    SupersContext context;

    public LuchasService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Lucha> Get() {
        return context.Luchas;
    }

    public async Task Save(Lucha lucha) {
        lucha.lucha_id = Guid.NewGuid();
        context.Add(lucha);
        context.SaveChanges();
    }

    public async Task Update(Guid id, Lucha lucha) {
        var luchaActual = context.Luchas.Find(id);

        if(luchaActual != null) {
            luchaActual.heroe_id = lucha.heroe_id;
            luchaActual.villano_id = lucha.villano_id;
            luchaActual.ganaHeroe = lucha.ganaHeroe;

            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var luchaActual = context.Luchas.Find(id);

        if(luchaActual != null) {
            context.Remove(luchaActual);
            context.SaveChanges();
        }
    }
}

public interface ILuchasService {
    IEnumerable<Lucha> Get();
    Task Save(Lucha lucha);

    Task Update(Guid id, Lucha lucha);

    Task Delete(Guid id);
}