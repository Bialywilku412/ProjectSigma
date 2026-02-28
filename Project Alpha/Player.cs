internal class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;

    public Player(string name, Weapon currentWeapon, Location currentLocation)
    {
        Name = name;
        MaximumHitPoints = 100;
        CurrentHitPoints = 100;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
    }

    public int Attack() => World.RandomGenerator.Next(1, CurrentWeapon.MaximumDamage + 1);
    public bool IsAlive() => CurrentHitPoints > 0;
    public void MoveTo(Location newLocation) => CurrentLocation = newLocation;
    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;

        if (CurrentHitPoints < damage)
        {
            CurrentHitPoints = 0;
        }
    }

}