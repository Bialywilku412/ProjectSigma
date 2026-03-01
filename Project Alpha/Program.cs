static class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Hero's name");
        string playerName = Console.ReadLine() ?? "Bob"; // A default name

        Player player = new Player(playerName, Weapon.Weapons[0], World.LocationByID(1));

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
            Console.WriteLine("What would you like yo do (Enter number)?");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    SeeGameStats(player);
                    break;
                case 2:
                    Move(player);
                    break;
            }
        }
    }

    static void SeeGameStats(Player player)
    {
        Console.WriteLine($"{player.Name}: {player.CurrentHitPoints}/{player.MaximumHitPoints}");
        Console.WriteLine($"Current weapon: {player.CurrentWeapon} Damage:1-{player.CurrentWeapon.MaximumDamage}");
    }

    static void Move(Player player)
    {
        Console.WriteLine("Where would you like to go?");
        Console.WriteLine($"You are at: {player.CurrentLocation}. From here you can go:");
        // Location functions has to be implemented yet
    }
}