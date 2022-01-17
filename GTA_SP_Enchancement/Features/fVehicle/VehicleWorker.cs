using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Features.fVehicle
{
    internal class VehicleWorker: IVehicleWorker
    {
        private IVehicle iVehicle;
        public VehicleWorker(IVehicle iVehicle)
        {
            this.iVehicle = iVehicle;
        }
        public void createFuelDisplay(Rage.Vehicle selectedVehicle)
        {
            selectedVehicle.FuelLevel -= (selectedVehicle.Speed / 100);
            this.iVehicle.displayFuel(selectedVehicle.FuelLevel);
        }
        public void createActionForEnterVehicle(Rage.Vehicle selectedVehicle)
        {
            if ((int)selectedVehicle.LockStatus == 7)
            {
                Rage.Game.DisplayNotification("You Hijack a car!");
                Rage.Ped ents = (Rage.Ped)Rage.World.GetClosestEntity(Rage.Game.LocalPlayer.Character.Position, 150f, Rage.GetEntitiesFlags.ConsiderHumanPeds);
                if (ents != null)
                {
                    ents.Tasks.FightAgainst(Rage.Game.LocalPlayer.Character, 30000);
                    Rage.Game.LocalPlayer.WantedLevel = 3;
                } else
                {

                    Rage.Game.DisplayNotification("Ents Nil!");
                }
                this.iVehicle.displayOutputIfPlayerSuccessInsideVehicle();
            } else if (selectedVehicle.HasDriver)
            {
                selectedVehicle.LockStatus = Rage.VehicleLockStatus.Locked;
                this.iVehicle.displayOutputIfPlayerSuccessInsideVehicle();
            }
            /*
            Rage.GameFiber.StartNew(
                new System.Threading.ThreadStart(
                    () =>
                    {
                        do
                        {

                        } while (Rage.Game.LocalPlayer.Character.IsInAnyVehicle(true));
                    }));
            */
        }
    }
}
