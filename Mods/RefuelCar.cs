using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Native;

namespace GTA_SP_Enchancement.Mods
{
    internal class RefuelCar
    {
        Vehicle lVehicle;
        Entity entity;
        public static RefuelCar init(Entity entity)
        {
            RefuelCar refuelCar = new RefuelCar();
            refuelCar.entity = entity;
            return refuelCar;
        }
        public Boolean startRefuel()
        {
            lVehicle = Game.LocalPlayer.LastVehicle;
            if (lVehicle != null)
            {
                float distance = this.entity.DistanceTo(lVehicle.Position);
                if (distance < 3.0f)
                {
                    Game.LocalPlayer.Character.Tasks.PlayAnimation("timetable@gardener@filling_can", "gar_ig_5_filling_can", 1, AnimationFlags.RagdollOnCollision);
                    GameFiber.Sleep(4000);
                    Game.LocalPlayer.Character.Tasks.Clear();
                    lVehicle.FuelLevel = 100f;
                    Game.DisplayNotification("Refuel Finish -100 will be deducted from " + Game.GetHashKey("SP1_TOTAL_CASH"));
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, -200);
                } else
                {
                    Game.DisplayNotification("Your car is too far!");
                }
            } else
            {
                Game.Console.Print("Vehicle Last Null! . somehow");
            }
            return true;
        }
        public void checkPositionWith()
        {

        }
    }
}
