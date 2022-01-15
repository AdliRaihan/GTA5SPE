using System;
using Rage;
using System.Threading;
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
                    Game.RawFrameRender += displayFuel;
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
            Vector2 startingPoint = new Vector2(xBase, 50);
            Vector2 endPoint = new Vector2(xBase + this.vNeeds.vehicle.FuelLevel, 50);
            Vector2 startingPointOverlay = new Vector2(xBase, 50);
            Vector2 endPointOverlay = new Vector2(xBase + 100.0f, 50);
            e.Graphics.DrawLine(startingPointOverlay, endPointOverlay, System.Drawing.Color.Black);
            e.Graphics.DrawLine(startingPoint, endPoint, System.Drawing.Color.Red);
            e.Graphics.DrawText(
                this.vNeeds.vehicle.FuelLevel.ToString(), 
                "Times New Roman",
                14.0f, 
                new System.Drawing.PointF(xBase, 30), 
                System.Drawing.Color.Red);
        }
        private void decreaseFuelLevel()
        {
            if (this.vNeeds.vehicle == null) return;
            if (this.vNeeds.vehicle.Speed == 0) return;
            if (vNeeds.vehicle.Speed > 80) vNeeds.vehicle.FuelLevel -= 6f; 
            else if (vNeeds.vehicle.Speed > 40) vNeeds.vehicle.FuelLevel -= 3f;
            else if (vNeeds.vehicle.Speed > 10) vNeeds.vehicle.FuelLevel -= 1.5f;
            else vNeeds.vehicle.FuelLevel -= 0.1f;
        }
    }
}
