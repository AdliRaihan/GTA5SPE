using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement
{
    public static class AppConstants
    {
        public static int globalTimeOut = 4000;
        public static int globalTimeSleepForNextEvent = 100;
        public static int globalTimeSleepForEventKey = 10;
        public static int globalAnimationTimeout = 10000;
    }
    public static class AppObjectConstants
    {
        public static PlayerAction findObject(string keyModel)
        {
            Rage.Game.DisplayNotification("Finding HASH => " + keyModel);
            var obj = new Dictionary<string, PlayerAction>()
            {
                { "0x885c12c7", PlayerAction.refuelCar },
                { "0x7339e883", PlayerAction.refuelCar },
                { "0x4fd621bc", PlayerAction.refuelCar },
                { "0x54bba095", PlayerAction.eat },
                { "PROP_VEND_COFFE_01", PlayerAction.drink },
                { "PROP_VEND_COFFE_02", PlayerAction.drink },
                { "PROP_VEND_COFFE_03", PlayerAction.drink },
                { "PROP_JUICE_DISPENSER", PlayerAction.drink },
                { "0xf2df798f", PlayerAction.drink },
                { "0x55b2bf3c", PlayerAction.drink },
            };
            if (obj.ContainsKey(keyModel))
                return obj[keyModel];
            else
                return PlayerAction.noAction;
        }
    }
    public static class AppAnimation
    {
        public static string baseEating = "amb@code_human_wander_eating_donut_fat@male@idle_a";
        public static string baseEating1 = "idle_a";
        //
        public static string baseDrink = "mp_player_intdrink";
        public static string baseDrinkIntro = "intro";
        public static string baseDrinkLoop = "loop";
        public static string baseDrinkOutro = "outro";
        //
        public static string baseShopping = "mp_am_hold_up";
        public static string baseShoppingPurchase = "purchase_cigarette_shopkeeper";
    }
    public enum PlayerAction
    {
        noAction = -99,
        refuelCar = 0,
        hunting = 1,
        eat = 2,
        drink = 3,
        scavenger = 4
    }
    public class playerHash
    {
        // Fuck yea!
        // fucking c# man
        public static dynamic zero = 0x324C31D;
        public static dynamic one = 0x44BD6982; 
        public static dynamic two = 0x8D75047D;
        public static dynamic getFrom(PlayerSP psp)
        {
            switch (psp)
            {
                case PlayerSP.PLAYER_ZERO:
                    return playerHash.zero;
                case PlayerSP.PLAYER_TWO:
                    return playerHash.two;
                default:
                    return playerHash.one;
            }
        }
    }
    public enum PlayerSP
    {
        PLAYER_ZERO,
        PLAYER_ONE,
        PLAYER_TWO
    }
    public class ObjectRarity
    {
        public static float N = 65.0f;
        public static float B = 35.0f;
        public static float A = 10.0f;
        public static float AA = 5.0f;
        public static float S = 4.50f;
        public static float SS = 0.1f;
        public static float SSS = 0.0001f;
    }
}
