namespace Core.DbModels;

public class Battle
{
    public int Id { get; set; }
    public List<Warrior> People { get; set; } = new List<Warrior>();
    public DateTime Date { get; set; }
}