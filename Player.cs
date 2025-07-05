public class Player
{
    public int X { get; set; }
    public int Y { get; set; }
    public Inventory Inventory { get; private set; }
    public bool HasAxe { get; set; }

    public Player(int x, int y)
    {
        X = x;
        Y = y;
        Inventory = new Inventory();
        HasAxe = true; // gracz zaczyna z siekierą
    }
}
