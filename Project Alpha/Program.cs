static class Program
{
    static void Main()
    {
        int restAmountOver = 3;
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
                "[3] - Inventory Check\n" +
                "[4] - Rest\n" +
                "[5] - to be expanded\n"
            );
            Console.WriteLine(player.CurrentQuest == null ? "NO QUEST" : "HAS QUEST");

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
                        if (player.CurrentLocation.MonsterLivingHere != null && player.CurrentQuest != null)
                        {
                            Battle(player.CurrentLocation.MonsterLivingHere, player);
                        }


                        string direction = Console.ReadLine() ?? "";
                        string move = player.Move(direction) ? $"You've travelled towards the {player.CurrentLocation}." : $"You are unable to travel to the {direction}.";
                        Console.WriteLine(move);

                        break;
                    case 3:
                        Console.WriteLine("=======Inventory=======");
                        Console.WriteLine("Weapons");
                        Console.WriteLine("Consumables");
                        Console.Write("\nWhich would you like to see: ");
                        string category = Console.ReadLine()!;
                        Console.WriteLine($"\n {player.OpenInventory(category)}");
                        if (category.ToLower() == "weapons")
                        {
                            Console.Write("Do you want to change you current weapon (Yes / No): ");
                            string choice = Console.ReadLine().ToLower();
                            switch (choice)
                            {
                                case "yes":
                                    Console.Write("Which weapon do you want to equip (Name): ");
                                    string weapon = Console.ReadLine()!;
                                    Console.WriteLine(player.EquipWeapon(weapon) ? $"\nSuccsesfully equiped {weapon}\n" : $"\nUnable to equip {weapon}\n");
                                    break;
                                default:
                                    Console.WriteLine(choice == "no" ? $"\nReturning to main menu\n" : $"\nInvalid input\n");
                                    break;
                            }
                        }
                        else if (category.ToLower() == "Consumables")
                        {
                            Console.Write("Do you want to a potion (Yes / No): ");
                            string choice = Console.ReadLine().ToLower();
                            switch (choice)
                            {
                                case "yes":
                                    Console.Write("Which potion do you want to use (Name): ");
                                    string potion = Console.ReadLine()!;
                                    Console.WriteLine(player.usePotion(potion) ? $"\nSuccsesfully equiped {potion}\n" : $"\nUnable to equip {potion}\n");
                                    break;
                                default:
                                    Console.WriteLine(choice == "no" ? $"\nReturning to main menu\n" : $"\nInvalid input\n");
                                    break;
                            }
                        }
                        break;
                    case 4:
                        if (restAmountOver > 0)
                        {
                            player.CurrentHitPoints += 30;
                            restAmountOver--;
                            Console.WriteLine($"You have recovered, rests over: {restAmountOver}");
                        }
                        else
                        {
                            Console.WriteLine("You can't rest anymore");
                        }
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
        Console.WriteLine($"Current Quest:{player.CurrentQuest.Name}");
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
                CompleteQuest(p, m);

                if (m.ID == World.MONSTER_ID_GIANT_SPIDER)
                {
                    Console.WriteLine("Congratulations, you won the game!");
                    Environment.Exit(0);
                }

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

    //Daniel Reward System
    public static void CompleteQuest(Player p, Monster m)
    {
        if (p.CurrentQuest == null)
            return;

        if (p.CurrentQuest.ID == World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN && m.ID == World.MONSTER_ID_RAT)
        {
            Console.WriteLine("Quest completed: Clear the Alchemist's Garden!");

            Console.WriteLine("Reward: Club weapon!");

            p.AddWeapon(World.WeaponByID(World.WEAPON_ID_CLUB));

            p.CompletedQuests++;
            p.CurrentQuest = null;
        }
    }

    //static void Move(Player player)
    //{
    //    Console.WriteLine("Where would you like to go?");
    //    Console.WriteLine($"You are at: {player.CurrentLocation}. From here you can go:");
    //    // Location functions has to be implemented yet
    //}
}