using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA_SP_Enchancement.Mods;
namespace GTA_SP_Enchancement
{
    internal class Allocations
    {
        System.Threading.ThreadStart characterNeedsMods, vehicleNeedsMods, behaviourCharacter;
        public void Run()
        {
            runBehaviour();
        }
        private void runBehaviour()
        {
            Behaviour behaviour = new Behaviour();
            behaviourCharacter = new System.Threading.ThreadStart(behaviour.RunModule);
            Rage.GameFiber.StartNew(behaviourCharacter);
        }
    }
}
