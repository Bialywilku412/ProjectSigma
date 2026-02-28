public class Weapon
{
    public int Id;
    public int MaximumDamage;
    public string Name;

    public Weapon(int id, string name, int maximumdamage)
    {
        Id = id;
        Name = name;
        MaximumDamage = maximumdamage;
    }

    public static List<Weapon> Weapons = new List<Weapon>()
    {
        new Weapon(1, "Rusty Sword", 2)

    };

}