namespace Core.DbModels;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Man> People { get; set; } = new List<Man>();
}