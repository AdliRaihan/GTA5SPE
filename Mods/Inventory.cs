using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using System.Drawing;
namespace GTA_SP_Enchancement.Mods
{
    internal class Inventory
    {
        public Boolean isInventoryActive = false;
        private float leftX, bottomY, padding, blockHeight;
        private float baseHeight = 44.0f;
        private float fontPadding = 16.0f;
        private RectangleF rectI1, rectI2, rectI3, rectI4, rectI5, rectI6, rectI7, rectI8;
        public void showInventory()
        {
            blockHeight = baseHeight - 6;
            padding = baseHeight - blockHeight;
            
            Game.RawFrameRender -= DisplayInventory;
            Game.RawFrameRender -= DisplayInventory1;
            Game.RawFrameRender -= DisplayInventory3;
            Game.RawFrameRender -= DisplayInventory4;
            Game.RawFrameRender -= DisplayInventory5;
            Game.RawFrameRender -= DisplayInventory6;
            Game.RawFrameRender -= DisplayInventory7;
            Game.RawFrameRender -= DisplayInventory8;

            Game.RawFrameRender += DisplayInventory;
            Game.RawFrameRender += DisplayInventory1;
            Game.RawFrameRender += DisplayInventory2;
            Game.RawFrameRender += DisplayInventory3;
            Game.RawFrameRender += DisplayInventory4;
            Game.RawFrameRender += DisplayInventory5;
            Game.RawFrameRender += DisplayInventory6;
            Game.RawFrameRender += DisplayInventory7;
            Game.RawFrameRender += DisplayInventory8;
        }
        private void DisplayInventory(object sender, GraphicsEventArgs e)
        {
            bottomY = Game.Resolution.Height - 95.0f;
            leftX = Game.Resolution.Width - (Game.Resolution.Width / 1.5f);
            RectangleF rect = new RectangleF(
                leftX, 
                bottomY,
                (blockHeight * 8) + padding,
                baseHeight);
            e.Graphics.DrawRectangle(
                   rect,
                   Color.FromArgb(99, Color.Black));
        }
        private void DisplayInventory1(object sender, GraphicsEventArgs e)
        {
            rectI1 = new RectangleF(
                leftX + padding,
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI1,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI1.Width + rectI1.X;
            float yCurrent = rectI1.Height + rectI1.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory2(object sender, GraphicsEventArgs e)
        {
            rectI2 = new RectangleF(
                rectI1.X + (rectI1.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI2,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI2.Width + rectI2.X;
            float yCurrent = rectI2.Height + rectI2.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory3(object sender, GraphicsEventArgs e)
        {
            rectI3 = new RectangleF(
                rectI2.X + (rectI2.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI3,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI3.Width + rectI3.X;
            float yCurrent = rectI3.Height + rectI3.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory4(object sender, GraphicsEventArgs e)
        {
            rectI4 = new RectangleF(
                rectI3.X + (rectI3.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI4,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI4.Width + rectI4.X;
            float yCurrent = rectI4.Height + rectI4.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory5(object sender, GraphicsEventArgs e)
        {
            rectI5 = new RectangleF(
                rectI4.X + (rectI4.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI5,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI5.Width + rectI5.X;
            float yCurrent = rectI5.Height + rectI5.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory6(object sender, GraphicsEventArgs e)
        {
            rectI6 = new RectangleF(
                rectI5.X + (rectI5.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI6,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI6.Width + rectI6.X;
            float yCurrent = rectI6.Height + rectI6.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory7(object sender, GraphicsEventArgs e)
        {
            rectI7 = new RectangleF(
                rectI6.X + (rectI6.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI7,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI7.Width + rectI7.X;
            float yCurrent = rectI7.Height + rectI7.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
        private void DisplayInventory8(object sender, GraphicsEventArgs e)
        {
            rectI8 = new RectangleF(
                rectI7.X + (rectI7.Width + padding),
                bottomY + padding,
                blockHeight - padding,
                blockHeight - padding);
            e.Graphics.DrawRectangle(
                   rectI8,
                   Color.FromArgb(127, 140, 141));
            float xCurrent = rectI8.Width + rectI8.X;
            float yCurrent = rectI8.Height + rectI8.Y;
            e.Graphics.DrawText(
                "99+",
                "Times New Roman",
                14.0f,
                new PointF(xCurrent - fontPadding, yCurrent - padding - fontPadding),
                Color.Red);
        }
    }
}
