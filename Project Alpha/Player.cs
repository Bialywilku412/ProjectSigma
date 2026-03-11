using System.Net;

internal class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;
    public Quest CurrentQuest;
    public readonly Dictionary<string, Weapon> Inventory = new();
    public int CompletedQuests = 0;

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
        if (newLocation.ID == World.LOCATION_ID_BRIDGE && CompletedQuests < 2)
        {
            Console.WriteLine("\nThe guard stops you.");
            Console.WriteLine("\"You cannot cross the bridge yet.\"");
            Console.WriteLine("\"Come back after you complete 3 quests for the town.\"");
            return;
        }

        if (newLocation.ID == World.LOCATION_ID_BRIDGE && CompletedQuests == 2)
        {
            Console.WriteLine("\nGuard: \"I see you've helped the town. You may pass.\"");
        }
        this.CurrentLocation = newLocation;

        if (CurrentLocation.QuestAvailableHere != null)
        {
            if (CurrentQuest == null || CurrentQuest.ID != CurrentLocation.QuestAvailableHere.ID)
            {
                Console.WriteLine($"\nYou arived at {newLocation.Name}.");
                Console.WriteLine($"There is a Quest available {CurrentLocation.QuestAvailableHere.Name}");
                Console.WriteLine("Do you Acept this Quest? (yes/no)");
                string choice = Console.ReadLine().ToLower();

                if (choice == "yes")
                {
                    this.CurrentQuest = CurrentLocation.QuestAvailableHere;
                    Console.WriteLine($"Your quest journey begins now. {this.CurrentQuest.Name}");
                }
                else if (choice == "no")
                {
                    Console.WriteLine("You are about to abort the Quest are you really not man enough to take it?");
                    Console.WriteLine("yes / no");
                    string choice_2 = Console.ReadLine().ToLower();

                    if (choice_2 == "yes")
                    {
                        this.CurrentQuest = CurrentLocation.QuestAvailableHere;
                        Console.WriteLine($"Thats what we like to see you Quest start now. {this.CurrentQuest.Name}");
                    }
                    else
                    {
                        Console.WriteLine("You canceled the Quest chicken");
                    }
                }

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
                    MoveTo(CurrentLocation.LocationToSouth);
                    return true;
                }
                else
                {
                    return false;
                }
            case "north":
                if (CurrentLocation.LocationToNorth != null)
                {
                    MoveTo(CurrentLocation.LocationToNorth);
                    return true;
                }
                else
                {
                    return false;
                }
            case "west":
                if (CurrentLocation.LocationToWest != null)
                {
                    MoveTo(CurrentLocation.LocationToWest);
                    return true;
                }
                else
                {
                    return false;
                }
            case "east":
                if (CurrentLocation.LocationToEast != null)
                {
                    MoveTo(CurrentLocation.LocationToEast);
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

    public string Open_inventory()
    {
        string inventory = "=======Inventory=======\n";
        if (Inventory.Count() > 0)
        {
            foreach (Weapon weapon in Inventory.Values)
            {
                inventory += $" - {weapon.Name}\n";
            }
        }
        else
        {
            inventory += $"\n         Empty         \n";
        }
        return inventory;
    }

    public void Add_to_Inventory(Weapon weapon) => Inventory.Add(weapon.Name.ToLower(), weapon);

    public bool Equip_Weapon(string name)
    {
        if (Inventory.Keys.Contains(name))
        {
            Inventory.Add(CurrentWeapon.Name, CurrentWeapon);
            CurrentWeapon = Inventory[name];
            Inventory.Remove(name);
            return true;
        }
        return false;
    }
}