namespace Core.DbModels;

public class Warrior : Man
{
    public Weapon? Weapon { get; set; }
    public int? WeaponId { get; set; }
    public int Level { get; set; }
    public List<Battle> Battles { get; set; } = new List<Battle>();
}