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
                GameFiber.Wait(AppConstants.globalTimeOut);
            } while (CharacterNeedsActive);
        }
        private void _ThristModifier()
        {
            if (this.cNeeds.player.Character.IsSprinting)
            {
                this.cNeeds.thrist -= 1f;
            }
            else if (this.cNeeds.player.Character.IsRunning)
            {
                this.cNeeds.thrist -= 0.1f;
            }
            else if (this.cNeeds.player.Character.IsWalking)
            {
                this.cNeeds.thrist -= 0.01f;
            } 
        }
    }
}
