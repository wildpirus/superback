using System.Text.Json.Serialization;

namespace superback.Models;

public class Rasgo {
    public Guid rasgo_id {get; set;}
    public Guid super_id {get; set;}
    public string titulo {get;set;}
    public string tipo_rasgo {get; set;}
    
    [JsonIgnore]
    public virtual Super Super {get; set;}
}
