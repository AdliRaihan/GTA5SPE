using System;
using Rage;
using System.Drawing;
namespace GTA_SP_Enchancement.Common.Drawable
{
    public class DFuel
    {
        public float fuelLevel = 0.0f;
        private const float padding = 150.0f;
        private const float widthIndicator = 100.0f;
        public void displayFuel(object sender, GraphicsEventArgs e)
        {
            Vector2 startingPointOverlay = new Vector2(Game.Resolution.Width - padding, 50);
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, widthIndicator, 10.0f),
                 Color.FromArgb(99, Color.Black));
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, fuelLevel, 10.0f),
                 Color.FromArgb(99, Color.YellowGreen));
        }
    }
}
