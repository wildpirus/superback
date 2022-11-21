using System.Text.Json.Serialization;

namespace superback.Models;

public class Patrocinador {
    public Guid patrocinador_id {get; set;}
    public Guid heroe_id {get;set;}
    public string nombre {get; set;}
    public float monto {get; set;}
    public string origen_monto {get; set;}
    [JsonIgnore]
    public virtual Heroe Heroe {get; set;}
}