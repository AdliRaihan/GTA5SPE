using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Common.AppHelper
{
    public static class AppPlayer
    {
        public static dynamic zero = 0x324C31D;
        public static dynamic one = 0x44BD6982;
        public static dynamic two = 0x8D75047D;
        public static dynamic getFrom(String pModel)
        {
            switch (pModel)
            {
                case "PLAYER_ZERO":
                    return playerHash.zero;
                case "PLAYER_TWO":
                    return playerHash.two;
                default:
                    return playerHash.one;
            }
        }
        public static void setMoney(String pModel, int val)
        {
            int money = AppPlayer.getMoney(pModel) + val;
            dynamic modelId = AppPlayer.getFrom(pModel);
            Rage.Native.NativeFunction.Natives.STAT_SET_INT<int>(modelId, money, -1);
        }
        public static int getMoney(String pModel)
        {
            int money = 0;
            dynamic modelId = AppPlayer.getFrom(pModel);
            Rage.Native.NativeFunction.Natives.STAT_GET_INT(modelId, ref money, -1);
            return money;
        }
    }
    public static class findObject
    {
        public static AppConstants.PlayerAction byName(string nameObj)
        {
            if (AppConstants.AppObj.name.ContainsKey(nameObj))
                return AppConstants.AppObj.name[nameObj];
            return AppConstants.PlayerAction.noAction;
        } 
    }
    public static class findPeds
    {
        public static AppConstants.PlayerAction byName(string nameObj)
        {
            if (AppConstants.AppObj.entityPeds.ContainsKey(nameObj))
                return AppConstants.AppObj.entityPeds[nameObj];
            return AppConstants.PlayerAction.noAction;
        }
    }
    public static class findLoc
    {
    }
    public static class playAnim
    {
        public static void upperBodyOnlyAssignTo(Rage.Ped target, string dict, string name, int timeout = -1)
        {
            Rage.GameFiber.StartNew(new System.Threading.ThreadStart(() =>
           {
               Rage.Native.NativeFunction.Natives.TASK_PLAY_ANIM(
                   target, dict, name,
                   2.0f,
                   -2.0f,
                   -1,
                   48,
                   0f,
                   true,
                   false,
                   true
                   );
               if (timeout > 0)
               {
                   Rage.GameFiber.Wait(timeout);
                   Rage.Game.LocalPlayer.Character.Tasks.Clear();
               }
           }));
        }
        public static void cancelAllAnimations()
        {
            Rage.Game.LocalPlayer.Character.Tasks.Clear();
        }
    }
    public static class NPC
    {
        public static void spawnStatic(Rage.Model model, Rage.Vector3 pos, float head = 45f)
        {
            var ped = new Rage.Ped(model, pos, head);
            ped.IsPersistent = true;
            ped.IsPositionFrozen = true;
            ped.IsInvincible = true;
            ped.CanRagdoll = false;
            ped.CanBeTargetted = false;
            ped.CanBeDamaged = false;
            ped.BlockPermanentEvents = true;
        }
    }
    public static class RNGesus
    {
        public static int calculateMe(int baseVal)
        {
            float randomForce = (float)new Random().NextDouble() * 100.0f;
            if (randomForce < ObjectRarity.SSS) return new Random().Next(1, baseVal * 1000000) * (int)randomForce;
            else if (randomForce < ObjectRarity.SS) return new Random().Next(1, baseVal * 100000) * (int)randomForce;
            else if (randomForce < ObjectRarity.S) return new Random().Next(1, baseVal * 10000) * (int)randomForce;
            else if (randomForce < ObjectRarity.AA) return new Random().Next(1, baseVal * 1000) * (int)randomForce;
            else if (randomForce < ObjectRarity.A) return new Random().Next(1, baseVal * 100) * (int)randomForce;
            else if (randomForce < ObjectRarity.B) return new Random().Next(1, baseVal * 10) * (int)randomForce;
            else if (randomForce < ObjectRarity.N) return new Random().Next(1, baseVal) * (int)randomForce;
            else return 0;
        }
        public static bool oddNumber()
        {
            float isOdd = new Random().Next(0, 100);
            return isOdd % 2 != 0;
        }
    }
}
