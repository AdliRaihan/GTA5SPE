using System;
using Rage;
namespace GTA_SP_Enchancement.SubFeatures
{
    internal class fSellingStatic
    {
        public static void sellCar()
        {
            Vehicle lV = Game.LocalPlayer.LastVehicle;
            if (lV != null)
                GameFiber.StartNew(new System.Threading.ThreadStart(() =>
                {
                    var paid = Common.AppHelper.RNGesus.calculateMe(100);
                    if (paid > 0)
                    {
                        Common.AppHelper.AppPlayer.setMoney(Game.LocalPlayer.Model.Name, paid);
                        lV.Delete();
                        Game.LocalPlayer.WantedLevel = 5;
                    }
                }));

        }
        public static void animalLoot(Entity entity)
        {
            if (entity != null)
                GameFiber.StartNew(new System.Threading.ThreadStart(() =>
                {
                    var paid = Common.AppHelper.RNGesus.calculateMe(10);
                    if (paid > 0)
                    {
                        Common.AppHelper.AppPlayer.setMoney(Game.LocalPlayer.Model.Name, paid);
                        entity.Delete();
                    }
                }));
        }
    }
}
