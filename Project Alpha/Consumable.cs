public class Consumable
{
    Player Player;
    public Dictionary<string, int> Consumables = new()
    {
        {"Healthpotion", 0},
        {"Strengthpotion", 0}
    };

    public Consumable(Player player) => Player = player;

    void Healthpotion() => Player.CurrentHitPoints += 10;

    void StrengthPotion() => Player.CurrentWeapon.MaximumDamage += 1;

    public bool UseConsumable(string name)
    {
        if (Consumables[name] > 0)
        {
            switch (name)
            {
                case "Healthpotion":
                    Healthpotion();
                    break;
                case "Strengthpotion":
                    StrengthPotion();
                    break;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    
}