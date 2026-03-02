public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Quest QuestAvailableHere;
    public Monster MonsterLivingHere;
    public Location LocationToNorth;
    public Location LocationToEast;
    public Location LocationToSouth;
    public Location LocationToWest;

    public Location(int iD, string name, string description, Quest questAvailableHere, Monster monasterLivingHere, Location locationToNorth = null, Location locationToEast = null, Location lopcationToSouth = null, Location locationToWest = null)
    {
        ID = iD;
        Name = name;
        Description = description;
        QuestAvailableHere = questAvailableHere;
        MonsterLivingHere = monasterLivingHere;
        LocationToNorth = locationToNorth;
        LocationToEast = locationToEast;
        LocationToSouth = lopcationToSouth;
        LocationToWest = locationToWest;
    }

    // shows surroundings, copy paste from code grade
    public string Compas()
    {
        string s = "From here you can go:\n";
        if (LocationToNorth != null)
        {
            s += "    N\n    |\n";
        }
        if (LocationToWest != null)
        {
            s += "W---|";
        }
        else
        {
            s += "    |";
        }
        if (LocationToEast != null)
        {
            s += "---E";
        }
        s += "\n";
        if (LocationToSouth != null)
        {
            s += "    |\n    S\n";
        }
        return s;
    }

    public override string ToString()
    {
        return Name;
    }
}