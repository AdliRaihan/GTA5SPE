using System;
using System.Windows.Forms;
using Rage;
namespace GTA_SP_Enchancement.Features.Cursor
{
    public class CursorControl: ICursor
    {
        public Boolean isCursorOn = false;
        // From Here
        public Common.AppConstants.AppThreadState controlState = Common.AppConstants.AppThreadState.onResume;
        public ICursor iCursor;
        private ICursorWorker Worker;
        private FeturesListenersDelegate DL;
        private System.Threading.ThreadStart MainTS;
        public CursorControl(FeturesListenersDelegate DL)
        {
            this.DL = DL;
            this.iCursor = this;
            this.Worker = new CursorWorker(this.iCursor);
        }
        public void RunInternally()
        {
            MainTS = new System.Threading.ThreadStart(iCursor.Run);
            GameFiber.StartNew(MainTS);
        }
        void ICursor.Run()
        {
            StartListener();
        }
        // Begin Logic Start Here!
        private void StartListener()
        {
            do
            {
                if (controlState != Common.AppConstants.AppThreadState.onHold)
                {
                    if (isCursorOn && Game.IsKeyDown(Keys.G))
                        GameFiber.StartNew(new System.Threading.ThreadStart(
                            () =>
                            {
                                var targetedEntity = Game.LocalPlayer.GetFreeAimingTarget();
                                if (targetedEntity != null)
                                    Worker.createFoundedEntity(Game.LocalPlayer.GetFreeAimingTarget());
                            }));
                    if (Game.IsKeyDown(Keys.B))
                    {
                        GameFiber.StartNew(new System.Threading.ThreadStart(
                            () =>
                            {
                                controlState = Common.AppConstants.AppThreadState.onHold;
                                isCursorOn = !isCursorOn;
                                this.Worker.createCursorState(isCursorOn);
                            }));
                    }
                    else if (Game.IsKeyDown(Keys.NumPad0))
                    {
                        Game.DisplayNotification(Game.LocalPlayer.Character.Position.ToString());
                    }
                    else if (Game.IsKeyDown(Keys.NumPad1))
                    {
                        var targetedEntity = Game.LocalPlayer.GetFreeAimingTarget();
                        if (targetedEntity != null)
                            Game.DisplayNotification(targetedEntity.Model.Name);
                    }
                }
                GameFiber.Sleep(Common.AppConstants.AppTimer.KeyListenerTimer);
            } while (controlState != Common.AppConstants.AppThreadState.released);
        }
        public void displayCursorState()
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            try {
                switch (isCursorOn)
                {
                    case true:
                        Game.RawFrameRender += Common.Drawable.DCursor.buildCursor;
                        break;
                    case false:
                        Game.RawFrameRender -= Common.Drawable.DCursor.buildCursor;
                        break;
                }
            }
            catch (Exception ex)
            {
                Game.LogTrivialDebug("ERROR: " + ex.Message);
            }
        }
        public void displayEntityForVehicle(Vehicle entity)
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            Common.AppConstants.PlayerAction action = Common.AppHelper.findObject.byName(entity.Model.Name);
            DL.CallController(new System.Collections.Generic.Dictionary<string, object>()
            {
                {"CursorControl", "Vehicle"},
                {"Entity", entity },
                {"Action", action }
            });
        }
        public void displayEntityForAnimals(Ped entity)
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            DL.CallController(new System.Collections.Generic.Dictionary<string, object>()
            {
                {"CursorControl", "Animals"},
                {"Entity", entity },
                {"Action", Common.AppConstants.PlayerAction.hunting }
            });
        }
        public void displayEntityForOtherPeds(Ped entity)
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            Common.AppConstants.PlayerAction action = Common.AppHelper.findPeds.byName(entity.Model.Name);
            DL.CallController(new System.Collections.Generic.Dictionary<string, object>()
            {
                {"CursorControl", "Ped"},
                {"Entity", entity },
                {"Action", action }
            });
        }
        public void displayEntityForOtherObject(Entity entity)
        {
            controlState = Common.AppConstants.AppThreadState.onResume;
            Common.AppConstants.PlayerAction action = Common.AppHelper.findObject.byName(entity.Model.Name);
            DL.CallController(new System.Collections.Generic.Dictionary<string, object>()
            {
                {"CursorControl", "Object"},
                {"Entity", entity },
                {"Action", action }
            });
        }
    }
}
