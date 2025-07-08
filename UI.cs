using System;

public static class UI
{
    private static bool showInventory = true;

    public static void ShowInventory(Player player)
    {
        if (!showInventory) return;

        Console.SetCursorPosition(0, 11);
        Console.WriteLine("Ekwipunek (max 3):");

        for (int i = 0; i < 3; i++)
        {
            string item = i < player.Inventory.Count ? player.Inventory[i] : "[Puste]";
            Console.WriteLine($"[{i + 1}] {item}");
        }

        Console.WriteLine("Q - wyrzuć | U - użyj (Boczek)");
    }

    public static void ToggleInventory() => showInventory = !showInventory;

    public static void ShowStamina(Player player)
    {
        Console.SetCursorPosition(0, 17);
        Console.Write("Stamina: ");
        int barWidth = 20;
        int filled = (int)((player.Stamina / (float)player.MaxStamina) * barWidth);

        Console.BackgroundColor = ConsoleColor.Green;
        Console.Write(new string(' ', filled));
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine(new string(' ', barWidth - filled));
    }
}
