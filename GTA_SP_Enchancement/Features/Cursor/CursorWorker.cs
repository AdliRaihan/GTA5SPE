using System;
using Rage;
using Rage.Native;

namespace GTA_SP_Enchancement.Features.Cursor
{
    internal class CursorWorker: ICursorWorker
    {
        private ICursor iCursor;
        public CursorWorker(ICursor iCursor)
        {
            this.iCursor = iCursor;
        }
        public void createCursorState(Boolean on = true)
        {
            NativeFunction.Natives.SET_PLAYER_FORCED_AIM(Game.LocalPlayer, on);
            iCursor.displayCursorState();
        }
        public void createFoundedEntity(Entity entity)
        {
            Game.Console.Print(entity.Model.Name);
            if (entity.Model.IsVehicle)
                this.iCursor.displayEntityForVehicle((Vehicle)entity);
            else if (entity.Model.IsPed)
            {
                if (entity.Model.Name.Contains("A_C"))
                    this.iCursor.displayEntityForAnimals((Ped)entity);
                else
                    this.iCursor.displayEntityForOtherPeds((Ped)entity);
            }
            else
                this.iCursor.displayEntityForOtherObject(entity);
        }
    }
}
