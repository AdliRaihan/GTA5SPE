using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using System.Windows.Forms;

namespace GTA_SP_Enchancement.Tools
{
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
                    Game.Console.Print(Game.LocalPlayer.GetFreeAimingTarget().ToString());
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
    }
}
