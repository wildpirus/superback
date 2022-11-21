namespace superback.Models;

public class Evento {
    public Guid evento_id {get; set;}
    public Guid heroe_id {get;set;}
    public string titulo {get; set;}
    public DateTime inicio {get; set;}
    public DateTime fin {get; set;}
    public string descripcion {get; set;}
    public string lugar {get; set;}
    public virtual Heroe Heroe {get; set;}
}