using System.Text.Json.Serialization;

namespace superback.Models;

public class Heroe {
    public Guid heroe_id {get; set;}
    public Guid super_id {get; set;}
    public virtual Super Super {get; set;}
    [JsonIgnore]
    public virtual ICollection<Lucha> Luchas {get; set;}
    //[JsonIgnore]
    public virtual ICollection<Evento> Eventos {get; set;}
    //[JsonIgnore]
    public virtual ICollection<Patrocinador> Patrocinadores {get; set;}
}