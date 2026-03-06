public class Monster
{
    public int ID;
    public string Name;
    public int MaximumDamage;
    public int CurrentHitPoints;
    public int MaximumHitPoints;

    public Monster(int id, string name, int maximumdamage, int currenthitpoints, int maximumhitpoints)
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumdamage;
        CurrentHitPoints = currenthitpoints;
        MaximumHitPoints = maximumhitpoints;
    }
    public static void attack(Monster m)
    {
        Random random = new Random();
        int attack = random.Next(1, m.MaximumDamage);
        Console.WriteLine($"{m.Name} attacked and striked! You lost: {attack}hp");
    }


}