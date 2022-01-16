using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Rage;
using System.Threading;

namespace GTA_SP_Enchancement.Mods
{
    public struct Needs
    {
        public float hunger;
        public float thrist;
        public float stress;
        public float tired;
        public Player player;
        public static Needs createInstanceCharacterNeeds()
        {
            Needs needs = new Needs();
            needs.hunger = 100;
            needs.thrist = 100;
            needs.stress = 100;
            needs.tired = 100;
            return needs;
        }
    }
    public interface CharacterNeedDelegate
    {
        void didCharacterUpdated(Needs currentCharacters); 
    }
    public class CharacterNeeds
    {
        public Boolean CharacterNeedsActive = true;
        private Needs cNeeds = Needs.createInstanceCharacterNeeds();
        public void RunModule()
        {
            try
            {
                this.cNeeds.player = Game.LocalPlayer;
                ThreadStart taskNeeds = new ThreadStart(this.CharacterNeedsUpdate);
                GameFiber.StartNew(taskNeeds);
            } catch (Exception ex)
            {
                Game.Console.Print("Exception Found!");
            } catch { 
                
            }
        }
        private void CharacterNeedsUpdate()
        {
            if (this.cNeeds.player == null)
            {
                Game.Console.Print("Player is not found!");
                return;
            }
            do
            {
                // Thrist
                this._ThristModifier();
                this.HungerModifier();
                GameFiber.Wait(AppConstants.globalTimeOut);
            } while (CharacterNeedsActive);
        }
        private void _ThristModifier()
        {
            Game.FrameRender -= displayThrist;
            if (this.cNeeds.thrist < 0.0f)
            {
                this.cNeeds.thrist = 50f;
                Game.DisplayNotification("You die cus no havent drink in a week");
                Game.LocalPlayer.Character.Kill();
            } else if (this.cNeeds.thrist < 5.0f)
            {
                if (this.cNeeds.player.Character.IsSprinting) this.cNeeds.thrist -= 0.5f;
                else if (this.cNeeds.player.Character.IsRunning) this.cNeeds.thrist -= 0.1f;
                else if (this.cNeeds.player.Character.IsWalking) this.cNeeds.thrist -= 0.05f;
                else this.cNeeds.thrist -= 0.1f;
            } else
            {
                if (this.cNeeds.player.Character.IsSprinting) this.cNeeds.thrist -= 5f;
                else if (this.cNeeds.player.Character.IsRunning) this.cNeeds.thrist -= 1f;
                else if (this.cNeeds.player.Character.IsWalking) this.cNeeds.thrist -= 0.25f;
                else this.cNeeds.thrist -= 0.1f;
            }
            try
            {
                Game.FrameRender += displayThrist;
            }
            catch (Exception ex) { }
            return;
        }
        private void HungerModifier()
        {
            Game.FrameRender -= displayHunger;
            if (this.cNeeds.hunger < 0.0f)
            {
                this.cNeeds.hunger = 50f;
                Game.DisplayNotification("You die cus no havent hungy in a week");
                Game.LocalPlayer.Character.Kill();
            } else if (this.cNeeds.hunger < 5.0f) this.cNeeds.hunger -= 0.1f;
            else this.cNeeds.hunger -= 0.5f;
            try
            {
                Game.FrameRender += displayHunger;
            } catch (Exception ex) { }
            return;
        }
        private void displayThrist(object sender, GraphicsEventArgs e)
        {
            if (this.cNeeds.player == null) return;
            float xBase = 20;
            float yBase = 75;
            Vector2 startingPointOverlay = new Vector2(xBase, yBase);
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, 100.0f, 10.0f),
                 Color.FromArgb(99, Color.Black));
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, this.cNeeds.thrist, 10.0f),
                 Color.FromArgb(99, Color.DarkGreen));
        }
        private void displayHunger(object sender, GraphicsEventArgs e)
        {
            if (this.cNeeds.player == null) return;
            float xBase = 20;
            float yBase = 100;
            Vector2 startingPointOverlay = new Vector2(xBase, yBase);
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, 100.0f, 10.0f),
                 Color.FromArgb(99, Color.Black));
            e.Graphics.DrawRectangle(
                new RectangleF(startingPointOverlay.X, startingPointOverlay.Y, this.cNeeds.hunger, 10.0f),
                 Color.FromArgb(99, Color.DarkGoldenrod));
        }

        // 
        public void addDrinkRefresh()
        {
            if (StaticUtil.getMoney(PlayerSP.PLAYER_ONE) < 125)
            {
                Game.DisplayNotification("You dont have money!");
                return;
            }
            Rage.Native.NativeFunction.Natives.TASK_PLAY_ANIM(
                Game.LocalPlayer.Character,
                AppAnimation.baseShopping,
                AppAnimation.baseShoppingPurchase,
                2.0f,
                -2.0f,
                -1,
                48,
                0f,
                true,
                false,
                true
                );
            GameFiber.Sleep(3 * 100);
            Rage.Native.NativeFunction.Natives.TASK_PLAY_ANIM(
                Game.LocalPlayer.Character,
                AppAnimation.baseDrink,
                AppAnimation.baseDrinkLoop,
                2.0f,
                -2.0f,
                -1,
                48,
                0f,
                true,
                false,
                true
                );
            if (this.cNeeds.thrist + 25.0f >= 100.0f)
            {
                this.cNeeds.thrist = 100.0f;
            } else
            {
                this.cNeeds.thrist += 25.0f;
            }
            StaticUtil.setMoney(PlayerSP.PLAYER_ONE, -125);
        }
        public void addHungerRefresh()
        {
            if (StaticUtil.getMoney(PlayerSP.PLAYER_ONE) < 125)
            {
                Game.DisplayNotification("You dont have money!");
                return;
            }
            Rage.Native.NativeFunction.Natives.TASK_PLAY_ANIM(
                Game.LocalPlayer.Character,
                AppAnimation.baseShopping,
                AppAnimation.baseShoppingPurchase,
                2.0f,
                -2.0f,
                -1,
                48,
                0f,
                true,
                false,
                true
                );
            GameFiber.Sleep(3 * 100);
            Rage.Native.NativeFunction.Natives.TASK_PLAY_ANIM(
                Game.LocalPlayer.Character,
                AppAnimation.baseEating,
                AppAnimation.baseEating1,
                2.0f,
                -2.0f,
                -1,
                48,
                0f,
                true,
                false,
                true
                );
            if (this.cNeeds.hunger + 25.0f >= 100.0f)
            {
                this.cNeeds.hunger = 100.0f;
            }
            else
            {
                this.cNeeds.hunger += 25.0f;
            }
            StaticUtil.setMoney(PlayerSP.PLAYER_ONE, -125);
        }
    }
}
