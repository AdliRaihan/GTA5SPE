using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Common.AppConstants
{
    public static class AppTimer
    {
        public static int KeyListenerTimer => 10;
        public static int EventListenerTimer => 100;
        // Depends on CPU
        public static int ObjectListenerTimer => 5000;
        // Giving Animation Time
        public static int ObjectAnimation => 5000;
    }
    public enum AppThreadState
    {
        onHold, onResume, released
    }
    public enum PlayerAction
    {
        noAction = -99,
        refuelCar = 0,
        hunting = 1,
        eat = 2,
        drink = 3,
        scavenger = 4,
        sellingCar = 5,
        carJob = 6,
    }
    public static class AppObj
    {
        public static Dictionary<string, PlayerAction> name => new Dictionary<string, PlayerAction>()
            {
                { "0x885c12c7", PlayerAction.refuelCar },
                { "0x7339e883", PlayerAction.refuelCar },
                { "0x4fd621bc", PlayerAction.refuelCar },
                { "0x64ff4c0e", PlayerAction.refuelCar },
                { "0x54bba095", PlayerAction.eat },
                { "PROP_VEND_COFFE_01", PlayerAction.drink },
                { "PROP_VEND_COFFE_02", PlayerAction.drink },
                { "PROP_VEND_COFFE_03", PlayerAction.drink },
                { "PROP_JUICE_DISPENSER", PlayerAction.drink },
                { "0xf2df798f", PlayerAction.drink },
                { "0x55b2bf3c", PlayerAction.drink },
            };
        public static Dictionary<string, PlayerAction> entityPeds => new Dictionary<string, PlayerAction>()
            {
                {  AppModel.carSeller, PlayerAction.sellingCar },
                {  AppModel.weazelNews, PlayerAction.carJob },
            };
    }
    public static class AppLocation
    {
        public enum Loc
        {
            sellThiefCar,
            carJob,
            carLoc1
        }
        public static Dictionary<Loc, Rage.Vector3> AllLoc => new Dictionary<Loc, Rage.Vector3>()
            {
               { Loc.sellThiefCar, new Rage.Vector3(1381.372f, -2079.216f, 50.99856f) },
                { Loc.carJob, new Rage.Vector3(-699.7123f, -919.6419f, 18.01391f) },
                { Loc.carLoc1, new Rage.Vector3(-454.4533f, -339.6561f, 33.36346f) }
            };
    }
    public static class AppModel
    {
        public static String carSeller => "S_M_M_AUTOSHOP_01";
        public static String weazelNews => "A_M_Y_KTOWN_01";
    }
}
