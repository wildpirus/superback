using Microsoft.EntityFrameworkCore;
using superback.Models;

namespace superback;

public class SupersContext: DbContext{
    public DbSet<Super> Supers {get; set;}
    public DbSet<Heroe> Heroes {get; set;}
    public DbSet<Villano> Villanos {get; set;}
    public DbSet<Rasgo> Rasgos {get; set;}
    public DbSet<Lucha> Luchas {get; set;}
    public DbSet<Patrocinador> Patrocinadores {get; set;}
    public DbSet<Evento> Eventos {get; set;}

    public SupersContext(DbContextOptions<SupersContext> options) :base(options) { 
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        List<Super> supersInit = new List<Super>();
        supersInit.Add(new Super() {
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
            nombre = "Gamora",
            edad = 22,
            relaciones = "Hijo de thanos",
            origen = "Titán"
        });
        supersInit.Add(new Super() {
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0549dfb4ef"),
            nombre = "Thanos",
            edad = 40,
            relaciones = "Hijo de eternos",
            origen = "Titán"
        });
        modelBuilder.Entity<Super>(super => {
            super.ToTable("super");
            super.HasKey(p=>p.super_id);
            super.Property(p=> p.nombre).IsRequired();
            super.Property(p=> p.edad).IsRequired();
            super.Property(p=> p.relaciones);
            super.Property(p=> p.origen);

            super.HasData(supersInit);
        });

        List<Rasgo> rasgosInit = new List<Rasgo>();
        modelBuilder.Entity<Rasgo>(rasgo => {
            rasgo.ToTable("rasgo");
            rasgo.HasKey(p=>p.rasgo_id);
            rasgo.HasOne(p=> p.Super).WithMany(p=> p.Rasgos).HasForeignKey(p=> p.super_id);
            rasgo.Property(p=> p.titulo).IsRequired();
            rasgo.Property(p=> p.tipo_rasgo).IsRequired();

            rasgo.HasData(rasgosInit);
        });


        List<Lucha> luchaInit = new List<Lucha>();
        modelBuilder.Entity<Lucha>(pelea => {
            pelea.ToTable("pelea");
            pelea.HasKey(p=>p.lucha_id);
            pelea.HasOne(p=>p.Heroe).WithMany(p=>p.Luchas).HasForeignKey(p=>p.heroe_id);
            pelea.HasOne(p=>p.Villano).WithMany(p=>p.Luchas).HasForeignKey(p=>p.villano_id);
            pelea.Property(p=> p.titulo);
            pelea.Property(p=>p.fecha);
            pelea.Property(p=> p.ganaHeroe).IsRequired();

            pelea.HasData(luchaInit);
        });

        List<Patrocinador> patrocinadorInit = new List<Patrocinador>();
        modelBuilder.Entity<Patrocinador>(patrocinador => {
            patrocinador.ToTable("patrocinador");
            patrocinador.HasKey(p=>p.patrocinador_id);
            patrocinador.HasOne(p=>p.Heroe).WithMany(p=>p.Patrocinadores).HasForeignKey(p=>p.heroe_id);
            patrocinador.Property(p=> p.nombre).IsRequired();
            patrocinador.Property(p=> p.monto).IsRequired();
            patrocinador.Property(p=> p.origen_monto).IsRequired();

            //patrocinador.HasData(patrocinadorInit);
        });

        List<Evento> eventoInit = new List<Evento>();
        eventoInit.Add(new Evento() {});
        modelBuilder.Entity<Evento>(evento => {
            evento.ToTable("evento");
            evento.HasKey(p=>p.evento_id);
            evento.HasOne(p=>p.Heroe).WithMany(p=>p.Eventos).HasForeignKey(p=>p.heroe_id);
            evento.Property(p=> p.titulo).IsRequired();
            evento.Property(p=> p.inicio).IsRequired();
            evento.Property(p=> p.fin).IsRequired();
            evento.Property(p=> p.descripcion);
            evento.Property(p=> p.lugar);

            //evento.HasData(eventoInit);
        });

        List<Heroe> heroeInit = new List<Heroe>();
        heroeInit.Add(new Heroe() {});
        modelBuilder.Entity<Heroe>(heroe => {
            heroe.ToTable("Heroe");
            heroe.HasKey(p=>p.heroe_id);
            heroe.HasOne<Super>(p => p.Super).WithOne(p => p.Heroe).HasForeignKey<Heroe>(p => p.super_id);
            //evento.HasData(eventoInit);
        });

        List<Villano> villanoInit = new List<Villano>();
        villanoInit.Add(new Villano() {});
        modelBuilder.Entity<Villano>(villano => {
            villano.ToTable("Villano");
            villano.HasKey(p=>p.villano_id);
            villano.HasOne<Super>(p => p.Super).WithOne(p => p.Villano).HasForeignKey<Villano>(p => p.super_id);
            //evento.HasData(eventoInit);
        });
    }

}