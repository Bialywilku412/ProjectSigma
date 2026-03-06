internal class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;
    public Quest CurrentQuest;

    public Player(string name, Weapon currentWeapon, Location currentLocation, Quest currentQuest)
    {
        Name = name;
        MaximumHitPoints = 100;
        CurrentHitPoints = 100;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
        CurrentQuest = currentQuest;
    }

    public int Attack() => World.RandomGenerator.Next(1, CurrentWeapon.MaximumDamage + 1);

    public bool IsAlive() => CurrentHitPoints > 0;

    public void MoveTo(Location newLocation)
    {
        CurrentLocation = newLocation;

        if (CurrentLocation.QuestAvailableHere != null)
        {
            if (CurrentQuest == null || CurrentQuest.ID != CurrentLocation.QuestAvailableHere.ID)
            {
                CurrentQuest = CurrentLocation.QuestAvailableHere;
                Console.WriteLine($"Your quest journey begins now. {this.CurrentQuest.Name}");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;

        if (CurrentHitPoints < 0)
        {
            CurrentHitPoints = 0;
        }
    }

    public bool Move(string direction)
    {
        switch (direction.ToLower())
        {
            case "south":
                if (CurrentLocation.LocationToSouth != null)
                {
                    CurrentLocation = CurrentLocation.LocationToSouth;
                    return true;
                }
                else
                {
                    return false;
                }
            case "north":
                if (CurrentLocation.LocationToNorth != null)
                {
                    CurrentLocation = CurrentLocation.LocationToNorth;
                    return true;
                }
                else
                {
                    return false;
                }
            case "west":
                if (CurrentLocation.LocationToWest != null)
                {
                    CurrentLocation = CurrentLocation.LocationToWest;
                    return true;
                }
                else
                {
                    return false;
                }
            case "east":
                if (CurrentLocation.LocationToEast != null)
                {
                    CurrentLocation = CurrentLocation.LocationToEast;
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
}