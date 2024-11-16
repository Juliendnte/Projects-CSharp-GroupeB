namespace ProjectsSharp.Service;

public class Tache
{
    public int Id { get; set; }         
    public string Titre { get; set; }
    public string Description { get; set; }
    public DateTime Date_debut { get; set; }
    public DateTime Date_fin { get; set; }
}