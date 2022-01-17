using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Features.fVehicle
{
    public struct VehicleBM
    {
    }
    public interface IVehicleWorker
    {
        void createFuelDisplay(Rage.Vehicle selectedVehicle);
        void createActionForEnterVehicle(Rage.Vehicle selectedVehicle);
    }
    public interface IVehicle
    {
        void displayFuel(float fuelLevel);
        void displayOutputIfPlayerSuccessInsideVehicle();
    }
}
