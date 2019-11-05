using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// CountingCombination class calculates the Upper total scores by
    /// taking the die value and multiplying it by the amount 
    /// of reoccurring same dice.
    /// </summary>
    [Serializable]
    class CountingCombination : Combination {
        private int dieValue;

        public CountingCombination(ScoreType score, Label combo) : base(combo) {
            // Sets dieValues equal to the clicked ScoreType button value.
            dieValue = (int)score + 1;            
        }

        public override int CalculateScore(int[] calcScore) {
            int count = 0;
            // Implements a search and compare algorithm.
            for (int i = 0; i < 5; i++) { 
                if (calcScore[i] == dieValue)
                    count++;
            }
            
            dieValue = (dieValue * count);

            Points = dieValue;

            return dieValue;


        }

        

    }
}
