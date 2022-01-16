using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace GTA_SP_Enchancement.Mods
{
    internal class EarnMoney
    {
        public static void MHunting(Entity entity)
        {
            if (entity.IsAlive) return;
            entity.Delete();
            float randomForce = (float)new Random().NextDouble() * 100.0f;
            Game.Console.Print(randomForce.ToString());
            if (randomForce < ObjectRarity.SSS) 
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 10000000) * (int) randomForce);
            else if (randomForce < ObjectRarity.SS)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 1000000) * (int)randomForce);
            else if (randomForce < ObjectRarity.S)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 100000) * (int)randomForce);
            else if (randomForce < ObjectRarity.AA)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 10000) * (int)randomForce);
            else if (randomForce < ObjectRarity.A)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 1000) * (int)randomForce);
            else if (randomForce < ObjectRarity.B)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 100) * (int)randomForce);
            else if (randomForce < ObjectRarity.N)
                StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 10) * (int)randomForce);
        }
        public static void Scavenger(Entity entity)
        {
            if (entity.Model.IsVehicle && Game.LocalPlayer.Character.DistanceTo(new Vector3(2204.0f, 3319f, 45.0f)) < 10)
            {
                Vehicle vecTarget = (Vehicle)entity;
                if (vecTarget.HasDriver == true) return;
                float randomForce = (float)new Random().NextDouble() * 100.0f;
                Game.Console.Print(randomForce.ToString());
                if (randomForce < ObjectRarity.SSS)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 100000000) * (int)randomForce);
                else if (randomForce < ObjectRarity.SS)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 10000000) * (int)randomForce);
                else if (randomForce < ObjectRarity.S)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 1000000) * (int)randomForce);
                else if (randomForce < ObjectRarity.AA)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 100000) * (int)randomForce);
                else if (randomForce < ObjectRarity.A)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 10000) * (int)randomForce);
                else if (randomForce < ObjectRarity.B)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 1000) * (int)randomForce);
                else if (randomForce < ObjectRarity.N)
                    StaticUtil.setMoney(PlayerSP.PLAYER_ONE, new Random().Next(1, 100) * (int)randomForce);
                entity.Delete();
                Game.DisplayNotification("You receive money from stealing car, but the owner call the policia, run!");
                Game.LocalPlayer.WantedLevel = 3;
            }
        }
    }
}
