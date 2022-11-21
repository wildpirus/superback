using System.Text.Json.Serialization;

namespace superback.Models;

public class Super {
    public Guid super_id {get; set;}
    public string nombre {get;set;}
    public int edad {get;set;}
    public string relaciones {get;set;}
    public string origen {get;set;}

    //[JsonIgnore]
    public virtual ICollection<Rasgo> Rasgos {get; set;}
    [JsonIgnore]
    public virtual Heroe Heroe {get; set;}
    [JsonIgnore]
    public virtual Villano Villano {get; set;}
}