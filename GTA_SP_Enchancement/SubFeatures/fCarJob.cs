using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.SubFeatures
{
    public class fCarJob
    {
        public bool isOnTheJob = false;
        Rage.Blip targetLoc;
        Rage.Vehicle targetVec;
        public void Contract()
        {
            int currentMoney = Common.AppHelper.AppPlayer.getMoney(Rage.Game.LocalPlayer.Character.Model.Name);
            if (currentMoney < 10000)
            {
                Rage.Game.DisplayNotification("You need atleast $10k to doing this mission!");
            }
            else if (Rage.Game.LocalPlayer.WantedLevel == 0)
            {
                targetVec = new Rage.Vehicle(new Rage.Model(0x187D938D), Common.AppConstants.AppLocation.AllLoc[Common.AppConstants.AppLocation.Loc.carLoc1]);
                targetVec.IsPersistent = true;
                targetVec.AttachBlip();
                isOnTheJob = true;
                Rage.GameFiber.StartNew(new System.Threading.ThreadStart(questListener));
                Common.AppHelper.AppPlayer.setMoney(Rage.Game.LocalPlayer.Model.Name, -10000);
            } else
                Rage.Game.DisplayNotification("Ditch the cops first yo!");
        }
        public void questListener()
        {
            var timer = Common.AppConstants.AppTimer.EventListenerTimer;
            do
            {
                Rage.Blip tBlip = targetVec.GetAttachedBlip();
                if (targetVec.EngineHealth < 100f)
                {
                    Rage.Game.DisplayNotification("You failed the mission/bet?, good luck!");
                    if (tBlip != null)
                        tBlip.Delete();
                    isOnTheJob = false;
                } else if (Rage.Game.LocalPlayer.Character.IsInVehicle(targetVec, true))
                {
                    Rage.Game.LocalPlayer.DispatchCops = true;
                    Rage.Game.LocalPlayer.WantedLevel = 3;
                    if (tBlip != null)
                        tBlip.Delete();
                    timer = 1500;
                } else
                {
                    if (tBlip == null && targetVec != null)
                        targetVec.AttachBlip();
                }
                Rage.GameFiber.Sleep(timer);
            } while (isOnTheJob);
        }
        public void questCheckerMarkDone()
        {
            if (isOnTheJob == false) return;
            if (Rage.Game.LocalPlayer.Character.IsInVehicle(targetVec, true))
            {
                Rage.Game.DisplayNotification("Get out from the vehicle!");
            } else if (!Rage.Game.LocalPlayer.AreWantedLevelStarsFlashing) {
                Rage.Game.DisplayNotification("HINT: You can't lose the cops while doing this mission but before you hand over the vehicle you need to make the cops doesnt see you!");
            } else
            {
                Rage.Vehicle lVec = Rage.Game.LocalPlayer.LastVehicle;
                if (lVec == targetVec)
                {
                    isOnTheJob = false;
                    Rage.Game.LocalPlayer.DispatchCops = false;
                    targetVec.IsPersistent = false;
                    targetVec.Delete();
                    var paid = Common.AppHelper.RNGesus.calculateMe(5000);
                    Common.AppHelper.AppPlayer.setMoney(Rage.Game.LocalPlayer.Model.Name, paid);
                }
            }
        }
    }
}
