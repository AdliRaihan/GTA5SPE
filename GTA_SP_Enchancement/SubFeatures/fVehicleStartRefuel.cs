using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace GTA_SP_Enchancement.SubFeatures
{
    internal class fVehicleStartRefuel
    {
        private Rage.Entity fuelTank;
        private Vehicle lV;
        private float cost;
        private bool isOnRef, isOnAnimation = false;
        public fVehicleStartRefuel(Rage.Entity fuelTank)
        {
            this.fuelTank = fuelTank;
        }
        public void startSubFeatures()
        {
            Game.Console.Print("Checking Last Vec!");
            lV = Game.LocalPlayer.LastVehicle;
            if (lV != null)
                GameFiber.StartNew(new System.Threading.ThreadStart(refuelCar));
        }
        public void refuelCar()
        {
            float distance = fuelTank.Position.DistanceTo(lV.Position);
            if (distance < 10.0f)
            {
                isOnRef = true;
                do
                {
                    if (isOnAnimation == false)
                    {
                        isOnAnimation = true;
                        GameFiber.StartNew(new System.Threading.ThreadStart(startRefuelAnimation));
                    }
                    GameFiber.Sleep(Common.AppConstants.AppTimer.KeyListenerTimer);
                } while (isOnRef);
            } else
            {
                Game.DisplayNotification("Your car is too far from your current Locations!");
            }
        } 
        public void startRefuelAnimation()
        {
            int currentMoney = Common.AppHelper.AppPlayer.getMoney(Game.LocalPlayer.Model.Name);
            Game.LocalPlayer.Character.Tasks.PlayAnimation("timetable@gardener@filling_can", "gar_ig_5_filling_can", 1, AnimationFlags.Loop);
            GameFiber.Wait(1000);
            do
            {
                if (Game.IsKeyDown(System.Windows.Forms.Keys.Space) || lV.FuelLevel >= 99 || currentMoney < cost)
                {
                    Game.DisplayNotification("Refuel finished you charged $" + (int)cost);
                    Common.AppHelper.AppPlayer.setMoney(Game.LocalPlayer.Model.Name, -((int)cost));
                    Common.AppHelper.playAnim.cancelAllAnimations();
                    isOnRef = isOnAnimation = false;
                }
                cost += 2.34f;
                lV.FuelLevel += 1;
                GameFiber.Sleep(250);
            } while (isOnAnimation);
        }
    }
}
