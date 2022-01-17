using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_SP_Enchancement.Features.Needs
{
    public class NeedsControl: FeaturesControlListeners
    {
        private FeturesListenersDelegate DL;
        private System.Threading.ThreadStart MainTS;
        public NeedsControl(FeturesListenersDelegate DL)
        {
            this.DL = DL;
            MainTS = new System.Threading.ThreadStart(this.Run);
        }
        private void Run()
        {

        }
    }
}
