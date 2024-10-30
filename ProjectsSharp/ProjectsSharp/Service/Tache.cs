namespace ProjectsSharp.Service;

public class Tache
{
    public int Id { get; set; }         
    public string Titre { get; set; }
    public string Description { get; set; }
    public DateTime date_debut { get; set; }
    public DateTime date_fin { get; set; }
}