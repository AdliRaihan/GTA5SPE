using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using Rage;
using Rage.Native;
using GTA_SP_Enchancement.Tools;
namespace GTA_SP_Enchancement.Mods
{
    internal class Behaviour : CursorDelegate
    {
        System.Threading.ThreadStart vehicleNeedsMods, toolsCursor, inventoryTS, characterNeedsTS;
        VehicleNeeds vehicleNeeds = new VehicleNeeds();
        CharacterNeeds characterNeeds = new CharacterNeeds();
        Tools.Cursor curTools = new Tools.Cursor();
        Inventory inventory = new Inventory();
        public void RunModule()
        {
            curTools.dGate = this;
            runNeeds();
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
                } else if (Game.IsKeyDown(Keys.NumPad1))
                {
                    Game.Console.Print(Game.LocalPlayer.Character.Position.ToString());
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
        private void runNeeds()
        {
            characterNeedsTS = new System.Threading.ThreadStart(characterNeeds.RunModule);
            GameFiber.StartNew(characterNeedsTS);
        }
        private void displayInventory()
        {
            this.inventory.showInventory();
        }
        void CursorDelegate.didCursorSelect(PlayerAction action, Entity selectedEntity)
        {
            Game.Console.Print(action.ToString());
            switch (action)
            {
                // Once go here thread will stopped until the current task done, atleast what i'm expect this to do
                case PlayerAction.refuelCar:
                    RefuelCar refCar = Mods.RefuelCar.init(selectedEntity);
                    GameFiber.WaitUntil(refCar.startRefuel);
                    break;
                case PlayerAction.eat:
                    characterNeeds.addHungerRefresh();
                    break;
                case PlayerAction.drink:
                    characterNeeds.addDrinkRefresh();
                    break;
                case PlayerAction.scavenger:
                    EarnMoney.Scavenger(selectedEntity);
                    break;
                case PlayerAction.hunting:
                    EarnMoney.MHunting(selectedEntity);
                    break;
                default:
                    break;
            }
        }

        public void hunting(Entity et)
        {

        }
        public void scavenger()
        {

        }
    }
}
