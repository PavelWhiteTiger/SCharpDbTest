namespace Core.DbModels;

public class Man
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string FullName => Name + " " + LastName;
    public DateTime DateOfBirth { get; set; }
    public Country? Country { get; set; }
    public int? CountryId { get; set; }
}