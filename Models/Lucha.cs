using System.Text.Json.Serialization;

namespace superback.Models;

public class Lucha {
    public Guid lucha_id {get; set;}
    public Guid heroe_id {get;set;}
    public Guid villano_id {get;set;}
    public string titulo {get;set;}
    public DateTime fecha {get; set;}
    public bool ganaHeroe {get; set;}
    //[JsonIgnore]
    public virtual Heroe Heroe {get; set;}
    public virtual Villano Villano {get; set;}
}