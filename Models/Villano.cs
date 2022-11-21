using System.Text.Json.Serialization;

namespace superback.Models;

public class Villano {
    public Guid villano_id {get; set;}
    public Guid super_id {get; set;}
    public virtual Super Super {get; set;}
    [JsonIgnore]
    public virtual ICollection<Lucha> Luchas {get; set;}
}