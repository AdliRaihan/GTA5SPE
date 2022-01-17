using Rage;
namespace GTA_SP_Enchancement.Common.Drawable
{
    public class DCursor
    {
        public static void buildCursor(object sender, GraphicsEventArgs e)
        {
            float centerX = Game.Resolution.Width / 2;
            float centerY = Game.Resolution.Height / 2;
            e.Graphics.DrawCircle(new Vector2(centerX, centerY), 1.0f, System.Drawing.Color.Red);
        }
    }
}
