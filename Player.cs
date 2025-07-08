using System.Collections.Generic;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }

    public List<string> Inventory = new List<string>();
    public int MaxInventorySize = 3;

    public int Stamina = 100;
    public int MaxStamina = 100;

    public Player(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void AddItem(string item)
    {
        if (Inventory.Count < MaxInventorySize)
            Inventory.Add(item);
    }

    public void RemoveItem(string item)
    {
        Inventory.Remove(item);
    }
}
