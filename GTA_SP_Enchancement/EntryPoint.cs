using System;
using Rage;
using GTA_SP_Enchancement;
[assembly: Rage.Attributes.Plugin("My First Plugin", Description = "This is my first plugin.", Author = "MyName")]
public static class EntryPoint
{
    public static void Main()
    {
        Game.Console.Print("Enchancement GP Plugin is loaded!");
        Allocations allocations = new Allocations();
        allocations.Run();
    }
}
