using System;
using Rage;
using GTA_SP_Enchancement.Common.AppHelper;
using GTA_SP_Enchancement.Common.AppConstants;
[assembly: Rage.Attributes.Plugin("My First Plugin", Description = "This is my first plugin.", Author = "MyName")]
public static class EntryPoint
{
    public static void Main()
    {
        /*
        Game.Console.Print("Enchancement GP Plugin is loaded!");
        Allocations allocations = new Allocations();
        allocations.Run();*/
        World.CleanWorld(true, true, true, true, true, false);
        var __main = new GTA_SP_Enchancement.Features.FeatureControllableThread();
        var model = new Model(AppModel.carSeller);
        NPC.spawnStatic(model, AppLocation.AllLoc[AppLocation.Loc.sellThiefCar], 0f);
        var modelS = new Model(AppModel.weazelNews);
        NPC.spawnStatic(modelS, AppLocation.AllLoc[AppLocation.Loc.carJob], 0f);
    }
}
