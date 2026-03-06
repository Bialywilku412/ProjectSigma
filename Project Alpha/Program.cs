static class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Hero's name");
        string playerName = Console.ReadLine() ?? "Bob"; // A default name

        Player player = new Player(playerName, World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD), World.LocationByID(1), null);

        // this change is purely for aestheticall reasons, leaving this commented out if you guys prefer this way
        /*
        Console.WriteLine($"Your name is {player.Name}. You live in a town called Riverbend");
        Console.WriteLine("You always dreamed to be a hero. And now when you heard that your town is being terrorized by big spiders.");
        Console.WriteLine("You decided to do all you can yo help your town");
        Console.WriteLine("In chest in your home there is a rusty sword that once your father fought with");
        Console.WriteLine("You take it and from now on you swear to protect people of your town");
        */

        Console.WriteLine(
            $"Your name is {player.Name}. You live in a town called Riverbend\n" +
            "You always dreamed to be a hero. And now when you heard that your town is being terrorized by big spiders.\n" +
            "You decided to do all you can yo help your town\n" +
            "In chest in your home there is a rusty sword that once your father fought with\n" +
            "You take it and from now on you swear to protect people of your town\n"
        );

        while (true)
        {
            Console.WriteLine(
                "[1] - Stats Check\n" +
                "[2] - Move\n" +
                "[3] - to be expanded\n"
            );

            Console.Write("What would you like to do?: ");

            bool validInput = int.TryParse(Console.ReadLine() ?? "0", out int numChoice);

            if (validInput)
            {
                switch (numChoice)
                {
                    case 1:
                        SeeGameStats(player);

                        break;
                    case 2:
                        Console.WriteLine(
                            "Where would you like to go?\n" +
                            $"You are at: {player.CurrentLocation}\n" +
                            $"{player.CurrentLocation.Compas()}");
                        if (player.CurrentLocation.MonsterLivingHere != null)
                        {
                            Battle(player.CurrentLocation.MonsterLivingHere, player);
                        }
                        

                        string direction = Console.ReadLine() ?? "";
                        string move = player.Move(direction) ? $"You've travelled towards the {player.CurrentLocation}." : $"You are unable to travel to the {direction}.";
                        Console.WriteLine(move);

                        break;
                    default:
                        Console.WriteLine("Invalid input, please enter a valid number");

                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number");
            }

        }
    }

    static void SeeGameStats(Player player)
    {
        Console.WriteLine($"{player.Name}: {player.CurrentHitPoints}/{player.MaximumHitPoints}");
        Console.WriteLine($"Current weapon: {player.CurrentWeapon.Name} Damage:1-{player.CurrentWeapon.MaximumDamage}");
    }


    //Daniel Battle logic
    public static void Battle(Monster m, Player p)
    {
        Console.WriteLine($"A {m.Name} appears!");

        while (m.CurrentHitPoints > 0 && p.IsAlive())
        {
            // Player attacks
            int playerDamage = p.Attack();
            Console.WriteLine($"{p.Name} Choose an action\n Attack - 1");
            string attack = Console.ReadLine();

            if (attack == "1") 
            {
                m.CurrentHitPoints -= playerDamage;

                Console.WriteLine($"You hit the {m.Name} for {playerDamage} damage.");

            }
            

            if (m.CurrentHitPoints <= 0)
            {
                Console.WriteLine($"You killed the {m.Name}!");
                p.CurrentLocation.MonsterLivingHere = null; //checks if the monster bit the dust
                break;
            }

            // Monster attacks
            int monsterDamage = World.RandomGenerator.Next(1, m.MaximumDamage + 1);
            p.TakeDamage(monsterDamage);

            Console.WriteLine($"The {m.Name} hits you for {monsterDamage} damage.");
            Console.WriteLine($"Your HP: {p.CurrentHitPoints}/{p.MaximumHitPoints}");

            if (!p.IsAlive())
            {
                Console.WriteLine("You died. Game over.");
                Environment.Exit(0);
            }
        }
    }

    //static void Move(Player player)
    //{
    //    Console.WriteLine("Where would you like to go?");
    //    Console.WriteLine($"You are at: {player.CurrentLocation}. From here you can go:");
    //    // Location functions has to be implemented yet
    //}
}