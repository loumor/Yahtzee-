using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Yahtzee_Game {
    /// <summary>
    /// Combination class passes the correct values and labels to the subclasses. 
    /// </summary>
    [Serializable]
    public abstract class Combination : Score {

        public Combination(Label combo) : base(combo) {
           

        }
        
        abstract public int CalculateScore(int[] calcScore);
        
        public void Sort (int[] calcScore) {
            Array.Sort(calcScore);
            // Passes the sorted array back to CalculateScore method.
            CalculateScore(calcScore); 
        }

    }
}
