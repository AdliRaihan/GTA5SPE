using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Rage.Native;

namespace GTA_SP_Enchancement
{
    internal class StaticUtil
    {
        public static void setMoney(PlayerSP model, int val)
        {
            int money = StaticUtil.getMoney(model) + val;
            dynamic modelId = playerHash.getFrom(model);
            NativeFunction.Natives.STAT_SET_INT<int>(modelId, money, -1);
        }
        public static int getMoney(PlayerSP model)
        {
            int money = 0;
            dynamic modelId = playerHash.getFrom(model);
            NativeFunction.Natives.STAT_GET_INT(modelId, ref money, -1);
            return money;
        }
    }
}
