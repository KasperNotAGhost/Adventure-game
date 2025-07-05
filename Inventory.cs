using System.Collections.Generic;

public class Inventory
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void Add(string item)
    {
        if (items.ContainsKey(item))
            items[item]++;
        else
            items[item] = 1;
    }

    public bool Remove(string item)
    {
        if (!items.ContainsKey(item) || items[item] == 0) return false;

        items[item]--;
        if (items[item] == 0)
            items.Remove(item);
        return true;
    }

    public Dictionary<string, int> GetItems()
    {
        return new Dictionary<string, int>(items);
    }
}
