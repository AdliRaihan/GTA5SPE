using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Features.Cursor
{
    public interface ICursorWorker
    {
        void createCursorState(Boolean on);
        void createFoundedEntity(Rage.Entity entity);
    }
    public interface ICursor
    {
        void Run();
        void displayCursorState();
        void displayEntityForVehicle(Rage.Vehicle entity);
        void displayEntityForAnimals(Rage.Ped entity);
        void displayEntityForOtherPeds(Rage.Ped entity);
        void displayEntityForOtherObject(Rage.Entity entity);
    }
}
