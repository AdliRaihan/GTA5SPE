using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using Rage;
using Rage.Native;

namespace GTA_SP_Enchancement.Mods
{
    internal class Behaviour
    {
        System.Threading.ThreadStart vehicleNeedsMods, toolsCursor, inventoryTS;
        VehicleNeeds vehicleNeeds = new VehicleNeeds();
        Tools.Cursor curTools = new Tools.Cursor();
        Inventory inventory = new Inventory();
        public void RunModule()
        {
            do
            {
                KeyboardState state = Game.GetKeyboardState();
                if (Game.IsKeyDown(Keys.F))
                {
                    if (Game.LocalPlayer.Character.VehicleTryingToEnter != null)
                    {
                        this.createLogicForEnterVehicle(() =>
                        {
                            this.runVehicleNeeds();
                        });
                    }
                    GameFiber.Sleep(AppConstants.globalTimeSleepForNextEvent);
                } else if (Game.IsKeyDown(Keys.G))
                {
                    this.curTools.cursorIsActive = !this.curTools.cursorIsActive;
                    NativeFunction.Natives.SET_PLAYER_FORCED_AIM(Game.LocalPlayer, this.curTools.cursorIsActive);
                    this.displayCursor();
                    GameFiber.Sleep(AppConstants.globalTimeSleepForNextEvent);
                } else if (Game.IsKeyDown(Keys.I))
                {
                    this.displayInventory();
                }
                GameFiber.Sleep(AppConstants.globalTimeSleepForEventKey);
            } while (true);
        }
        private void displayCursor()
        {
            if (!this.curTools.cursorIsActive) return;
            this.toolsCursor = new System.Threading.ThreadStart(this.curTools.RunModule);
            Rage.GameFiber.StartNew(this.toolsCursor);
        }
        public void createLogicForEnterVehicle(Action cb)
        {
            int timeOutFunction = 0;
            Vehicle vecTryingEnter = Game.LocalPlayer.Character.VehicleTryingToEnter;
            if (!vecTryingEnter.HasDriver)
            {
                do
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {

                        this.vehicleNeeds.vNeeds.vehicle = Game.LocalPlayer.Character.CurrentVehicle;
                        timeOutFunction = 999999999;
                    }
                    timeOutFunction += AppConstants.globalTimeOut;
                    GameFiber.Sleep(AppConstants.globalTimeOut);
                } while (AppConstants.globalAnimationTimeout >= timeOutFunction);
                cb();
            } else
            {
                Game.LocalPlayer.Character.VehicleTryingToEnter.LockStatus = VehicleLockStatus.Locked;
            }
        }
        private void runVehicleNeeds()
        {
            if (vehicleNeeds.VehicleNeedsActive) return;
            vehicleNeedsMods = new System.Threading.ThreadStart(vehicleNeeds.RunModule);
            Rage.GameFiber.StartNew(vehicleNeedsMods);
        }
        private void displayInventory()
        {
            this.inventory.showInventory();
        }
    }
}
