namespace Journey.Infrastructure.Entities;
public class Trip
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    //No banco de dados não existe essa lista de atividades.
    //Porém o bd já está configurado com a chave estrangeira.
    //E uma viagem pode ter N atividades.
    //O EntityFramework é esperto o bastante para fazer essa relação.
    public IList<Activity> Activities { get; set; } = [];
}
