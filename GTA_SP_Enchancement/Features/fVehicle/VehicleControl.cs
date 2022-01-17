using System;
using Rage;
using GTA_SP_Enchancement.Common.Drawable;
namespace GTA_SP_Enchancement.Features.fVehicle
{
    public class VehicleControl: IVehicle
    {
        private Boolean isInsideVehicle = false;
        public DFuel dFuel = new DFuel();
        // From Here
        public Common.AppConstants.AppThreadState controlState = Common.AppConstants.AppThreadState.onResume;
        public IVehicle iVehicle;
        private IVehicleWorker Worker;
        private FeturesListenersDelegate DL;
        private System.Threading.ThreadStart MainTS, SecondaryTS;
        public VehicleControl(FeturesListenersDelegate DL)
        {
            Game.Console.Print("Vec Module Active!");
            this.DL = DL;
            this.iVehicle = this;
            this.Worker = new VehicleWorker(this.iVehicle);
        }
        public void RunInternally()
        {
            MainTS = new System.Threading.ThreadStart(StartIfPlayerInsideVehicleListener);
            GameFiber.StartNew(MainTS);
        }
        private void StartIfPlayerInsideVehicleListener()
        {
            do
            {
                var timer = Common.AppConstants.AppTimer.ObjectListenerTimer;
                if (controlState != Common.AppConstants.AppThreadState.onHold)
                {
                    // Count as checking if current player inside vec
                    Vehicle cVehicle = Game.LocalPlayer.Character.CurrentVehicle;
                    // used for display/refresh
                    if (cVehicle != null)
                    {
                        isInsideVehicle = true;
                        GameFiber.StartNew(new System.Threading.ThreadStart(() =>
                        {
                            controlState = Common.AppConstants.AppThreadState.onHold;
                            Worker.createFuelDisplay(cVehicle);
                        }));
                    }
                    else
                    {
                        Game.RawFrameRender -= dFuel.displayFuel;
                        if (Game.IsKeyDown(System.Windows.Forms.Keys.F))
                        {
                            Vehicle tVehicle = Game.LocalPlayer.Character.VehicleTryingToEnter;
                            if (tVehicle != null && Game.LocalPlayer.Character.CurrentVehicle == null)
                            {
                                controlState = Common.AppConstants.AppThreadState.onHold;
                                Worker.createActionForEnterVehicle(tVehicle);
                            }
                        }
                        timer = Common.AppConstants.AppTimer.KeyListenerTimer;
                    }
                }
                GameFiber.Sleep(timer);
            } while (controlState != Common.AppConstants.AppThreadState.released);
        }
        public void displayFuel(float fuelLevel)
        {
            Game.RawFrameRender -= dFuel.displayFuel;
            dFuel.fuelLevel = fuelLevel;
            Game.RawFrameRender += dFuel.displayFuel;
            controlState = Common.AppConstants.AppThreadState.onResume;
        }
        public void displayOutputIfPlayerSuccessInsideVehicle()
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            isInsideVehicle = true;
            GameFiber.Wait(Common.AppConstants.AppTimer.ObjectListenerTimer);
        }
    }
}
