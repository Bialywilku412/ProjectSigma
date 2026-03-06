public class Weapon
{
    public int ID;
    public int MaximumDamage;
    public string Name;

    public Weapon(int id, string name, int maximumdamage)
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumdamage;
    }

    public static List<Weapon> Weapons = new List<Weapon>()
    {
        new Weapon(1, "Rusty Sword", 2)

    };
}