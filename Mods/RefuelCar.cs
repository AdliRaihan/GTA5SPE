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
            if (lVehicle != null && !Game.LocalPlayer.Character.IsInAnyVehicle(true))
            {
                float distance = this.entity.DistanceTo(lVehicle.Position);
                if (distance < 3.0f)
                {

                    Game.LocalPlayer.Character.Tasks.PlayAnimation("timetable@gardener@filling_can", "gar_ig_5_filling_can", 1, AnimationFlags.Loop);
                    // manipulate to think this function still in 1 thread cus it break the logic.
                    costOfFuel(lVehicle, (cost) =>
                    {
                        StaticUtil.setMoney(PlayerSP.PLAYER_ONE, -cost); // TODO: STILL PLAYER ONE
                    });
                } else
                {
                    Game.DisplayNotification("Your car is too far!");
                }
            } else
            {
                Game.Console.Print("There are no vehicle to fill!");
            }
            return true;
        }
        private void costOfFuel(Vehicle vec, Action<int> cb)
        {
            int cost = 0;
            int currentMoney = StaticUtil.getMoney(PlayerSP.PLAYER_ONE); // TODO: STILL PLAYER ONE
            if (currentMoney == 0)
            {
                Game.LocalPlayer.Character.Tasks.Clear(); return;
            }
            GameFiber.StartNew(new System.Threading.ThreadStart(() =>
            {
                int stopped = 0;
                while(stopped == 0)
                {
                    // Float not supported!
                    vec.FuelLevel += 1;
                    cost += 1;
                    GameFiber.Sleep(AppConstants.globalTimeSleepForEventKey);
                    // H
                    if (Game.IsKeyDown(System.Windows.Forms.Keys.Space) || vec.FuelLevel >= 99 || currentMoney < cost)
                    {
                        Game.LocalPlayer.Character.Tasks.Clear();
                        stopped++;
                    }
                }
                cb(cost);
            }));
        }
    }
}
