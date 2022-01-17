using GTA_SP_Enchancement.Features.Cursor;
using GTA_SP_Enchancement.Features.FeatureControl;
using GTA_SP_Enchancement.Features.Needs;
using GTA_SP_Enchancement.Features.fVehicle;
using System;
using System.Collections.Generic;

namespace GTA_SP_Enchancement.Features
{
    public class FeatureControllableThread : IFeatureControllableThread, FeturesListenersDelegate
    {
        public CursorControl _cursor => new CursorControl(this);
        public NeedsControl _needs => new NeedsControl(this);
        public VehicleControl _vehicle => new VehicleControl(this);
        private SubFeatures.fCarJob _FCJ;
        public FeatureControllableThread() {
            Rage.Game.Console.Print("Main Module Active!");
            Rage.GameFiber.StartNew(new System.Threading.ThreadStart(_cursor.RunInternally));
            Rage.GameFiber.StartNew(new System.Threading.ThreadStart(_vehicle.RunInternally));
        }
        public void CallController(Dictionary<string, object> information)
        {

            if (information.ContainsKey("CursorControl") && information.ContainsKey("Action") && information.ContainsKey("Entity"))
            {
                Common.AppConstants.PlayerAction PA = (Common.AppConstants.PlayerAction)information["Action"];
                switch (PA)
                {
                    case Common.AppConstants.PlayerAction.refuelCar:
                        var rfs = new SubFeatures.fVehicleStartRefuel((Rage.Entity)information["Entity"]);
                        rfs.startSubFeatures();
                        break;
                    case Common.AppConstants.PlayerAction.sellingCar:
                        SubFeatures.fSellingStatic.sellCar();
                        break;
                    case Common.AppConstants.PlayerAction.hunting:
                        SubFeatures.fSellingStatic.animalLoot(((Rage.Entity)information["Entity"]));
                        break;
                    case Common.AppConstants.PlayerAction.carJob:
                        handlingSubFeatureForCarJob();
                        break;
                    default:
                        break;
                }
            }
        }
        private void handlingSubFeatureForCarJob()
        {
            if (this._FCJ == null)
                this._FCJ = new SubFeatures.fCarJob();

            if (!this._FCJ.isOnTheJob)
            {
                this._FCJ.Contract();
            } else
            {
                this._FCJ.questCheckerMarkDone();
            }
        }
    }
}
