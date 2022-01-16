using System;
using Rage;
using System.Threading;
using System.Drawing;
using System.Windows.Input;
namespace GTA_SP_Enchancement.Mods
{
    internal class VehicleNeeds
    {
        public struct Vehicle
        {
            public Rage.Player player;
            public Rage.Vehicle vehicle;
            public Rage.Vehicle savedVehicle;
            public Rage.Vehicle pointedVehicle;
        }
        public Boolean VehicleNeedsActive = false;
        public Vehicle vNeeds = new Vehicle();
        public void RunModule()
        {
            try
            {
                vNeeds.player = Game.LocalPlayer;
                this.VehicleNeedsActive = true;
                ThreadStart taskNeeds = new ThreadStart(this.VehicleListener);
                GameFiber.StartNew(taskNeeds);
            }
            catch (Exception ex)
            {
                Game.Console.Print("Exception Found!");
            }
        }
        public void VehicleListener()
        {
            if (this.vNeeds.player == null)
            {
                Game.Console.Print("Player is not found!");
                return;
            }
            do
            {
                Game.RawFrameRender -= displayFuel;
                if (Game.LocalPlayer.Character.CurrentVehicle != null)
                {
                    this.vNeeds.vehicle = this.vNeeds.player.Character.CurrentVehicle;
                    try
                    {
                        Game.RawFrameRender += displayFuel;
                    }
                    catch (Exception ex) { }
                    this.decreaseFuelLevel();
                } else
                {
                    this.vNeeds.vehicle = null;
                    this.VehicleNeedsActive = false;
                }
                GameFiber.Wait(AppConstants.globalTimeOut);
            } while (this.VehicleNeedsActive);
        }
        private void displayFuel(object sender, GraphicsEventArgs e)
        {
            if (this.vNeeds.vehicle == null) return;
            float xBase = 20;
            Vector2 startingPointOverlay = new Vector2(Game.Resolution.Width - 150.0f, 50);
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, 100.0f, 10.0f),
                 Color.FromArgb(99, Color.Black));
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, this.vNeeds.vehicle.FuelLevel, 10.0f),
                 Color.FromArgb(99, Color.YellowGreen));
        }
        private void decreaseFuelLevel()
        {
            if (this.vNeeds.vehicle == null) return;
            if (this.vNeeds.vehicle.Speed == 0) return;
            if (vNeeds.vehicle.Speed > 80) vNeeds.vehicle.FuelLevel -= 2f; 
            else if (vNeeds.vehicle.Speed > 40) vNeeds.vehicle.FuelLevel -= 1f;
            else if (vNeeds.vehicle.Speed > 10) vNeeds.vehicle.FuelLevel -= 0.5f;
            else vNeeds.vehicle.FuelLevel -= 0.1f;
        }
    }
}
