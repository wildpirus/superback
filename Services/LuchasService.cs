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
        await context.AddAsync(lucha);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Lucha lucha) {
        var luchaActual = context.Luchas.Find(id);

        if(luchaActual != null) {
            luchaActual.heroe_id = lucha.heroe_id;
            luchaActual.villano_id = lucha.villano_id;
            luchaActual.ganaHeroe = lucha.ganaHeroe;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var luchaActual = context.Luchas.Find(id);

        if(luchaActual != null) {
            context.Remove(luchaActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface ILuchasService {
    IEnumerable<Lucha> Get();
    Task Save(Lucha lucha);

    Task Update(Guid id, Lucha lucha);

    Task Delete(Guid id);
}