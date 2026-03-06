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
                            $"{player.CurrentLocation.Compas()}"
                        );

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

    //static void Move(Player player)
    //{
    //    Console.WriteLine("Where would you like to go?");
    //    Console.WriteLine($"You are at: {player.CurrentLocation}. From here you can go:");
    //    // Location functions has to be implemented yet
    //}
}