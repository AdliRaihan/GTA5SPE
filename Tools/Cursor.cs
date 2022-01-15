using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using System.Windows.Forms;

namespace GTA_SP_Enchancement.Tools
{
    struct CursorAction
    {
        PlayerAction action;
        Entity target;
    }
    internal class Cursor
    {
        public Boolean cursorIsActive = false;
        public void RunModule()
        {
            Game.RawFrameRender += buildCursor;
            do
            {
                if (Game.IsKeyDown(Keys.E))
                {
                    Entity selectedO = Game.LocalPlayer.GetFreeAimingTarget();
                    if (selectedO != null)
                        switch (this.objectIdentifications(Game.LocalPlayer.GetFreeAimingTarget().Model.Name))
                        {
                            // Once go here thread will stopped until the current task done, atleast what i'm expect this to do
                            case PlayerAction.refuelCar:
                                Mods.RefuelCar refCar = Mods.RefuelCar.init(selectedO);
                                GameFiber.WaitUntil(refCar.startRefuel);
                                break;
                            default:
                                break;
                        }
                }
                GameFiber.Sleep(AppConstants.globalTimeSleepForEventKey);
                if (!cursorIsActive)
                    Game.RawFrameRender -= buildCursor;
            } while(cursorIsActive);
        }
        private void buildCursor(object sender, GraphicsEventArgs e)
        {
            float centerX = Game.Resolution.Width / 2;
            float centerY = Game.Resolution.Height / 2;
            e.Graphics.DrawCircle(new Vector2(centerX, centerY), 10.0f, System.Drawing.Color.Red);
        }
        private PlayerAction objectIdentifications(String modelName)
        {
            if (modelName == AppObjectConstants.fuelTank)
            {
                return PlayerAction.refuelCar;
            }
            return PlayerAction.noAction;
        }
    }
}
