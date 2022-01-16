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
    interface CursorDelegate
    {
        void didCursorSelect(PlayerAction action, Entity selectedEntity);
    }
    internal class Cursor
    {
        public Boolean cursorIsActive = false;
        public CursorDelegate dGate;
        public void RunModule()
        {
            Game.RawFrameRender += buildCursor;
            do
            {
                if (Game.IsKeyDown(Keys.E))
                {
                    Entity selectedO = Game.LocalPlayer.GetFreeAimingTarget();
                    if (selectedO != null)
                        this.dGate.didCursorSelect(this.objectIdentifications(Game.LocalPlayer.GetFreeAimingTarget()), selectedO);
                } else if (Game.IsKeyDown(Keys.NumPad0)) {
                    Entity selectedO = Game.LocalPlayer.GetFreeAimingTarget();
                    if (selectedO != null)
                        Game.DisplayNotification(selectedO.Model.Name);
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
            e.Graphics.DrawCircle(new Vector2(centerX, centerY), 1.0f, System.Drawing.Color.Red);
        }
        private PlayerAction objectIdentifications(Entity entity)
        {
            string modelName = entity.Model.Name;
            if (modelName.Contains("A_C") && entity.IsDead)
                return PlayerAction.hunting;
            else if (entity.Model.IsVehicle)
                return PlayerAction.hunting;
            else return AppObjectConstants.findObject(modelName);
        }
    }
}
