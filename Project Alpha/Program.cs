static class Program
{
    static void Main()
    {
        Console.WriteLine("Deafault setup");

        Console.WriteLine("Enter Hero's name");
        string playerName = Console.ReadLine();
        // Weapon and location has to be created yet
        Player player = new Player(playerName, "rusty sword", "home");
        while (true)
        {

        }

    }
}