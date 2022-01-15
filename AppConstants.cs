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
        public static int globalTimeSleepForNextEvent = 500;
        public static int globalTimeSleepForEventKey = 10;
        public static int globalAnimationTimeout = 10000;
    }
    public static class AppObjectConstants
    {
        public static String fuelTank = "0x885c12c7";
    }
    public enum PlayerAction
    {
        noAction = -99,
        refuelCar = 0
    }
    public class playerHash
    {
        public static dynamic zero = 0x324C31D;
        public static dynamic one = 0x44BD6982; // uint32/similar not working!
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
}
