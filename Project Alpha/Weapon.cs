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
}