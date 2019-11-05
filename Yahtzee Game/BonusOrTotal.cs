using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// BonusOrTotal class returns done as true so that the totals and subtotals
    /// labels are updated on the GUI.
    /// </summary>
    [Serializable]
    class BonusOrTotal : Score {
        
        public BonusOrTotal (Label BonusTotal) : base (BonusTotal) {
            
            done = true;
        }

    }
}
