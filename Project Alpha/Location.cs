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
}