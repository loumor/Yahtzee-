using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// FixedScore class calculates the Lower section totals and returns their value.
    /// </summary>
    [Serializable]
    class FixedScore : Combination {

        private ScoreType scoreType;

        public FixedScore(ScoreType fixedscore, Label combo) : base(combo) {
            // Sets the variable so it can be accessed in the following methods.
            scoreType = fixedscore; 
        }


        public override int CalculateScore(int[] calcScore) {
            // Checks each ScoreType and runs the correct method.
            // The caclScore array has also been sorted into ascending order from the combination class.
            if ((int)scoreType == 12) {
                return CalculateSmallStraight(calcScore);
            }
            if ((int)scoreType == 13) {
                return CalculateLargeStraight(calcScore);
            }
            if ((int)scoreType == 11) {
                return CalculateFullHouse(calcScore);
            }
            if ((int)scoreType == 15) {
                return CalculateYahtzee(calcScore);
            }
            else {
                return 0;
            }

        }
           
 
        /// <summary>
        /// Calculates the Full House score by checking for two of a kind
        /// and by checking for three of a kind, then gives the score value 
        /// of 25 if this is true.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateFullHouse(int[] calcScore) {
            int Sum = 0;
            
            if ((((calcScore[0] == calcScore[1]) && (calcScore[1] == calcScore[2])) && // Three of a Kind.
                 (calcScore[3] == calcScore[4]) && // Two of a Kind.
                 (calcScore[2] != calcScore[3])) ||
                ((calcScore[0] == calcScore[1]) && // Two of a Kind.
                 ((calcScore[2] == calcScore[3]) && (calcScore[3] == calcScore[4])) && // Three of a Kind.
                 (calcScore[1] != calcScore[2]))) {
                Sum = 25;
            }
            Points = Sum;
            return Sum;
        }

        /// <summary>
        /// Checks for a Large Straight by seeing if the order of the array is
        /// either 1,2,3,4,5  or 2,3,4,5,6, if this is true then the score 
        /// value of 40 is given.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateLargeStraight(int[] calcScore) {
            int Sum = 0;

            if (((calcScore[0] == 1) &&
                 (calcScore[1] == 2) &&
                 (calcScore[2] == 3) &&
                 (calcScore[3] == 4) &&
                 (calcScore[4] == 5)) ||
                ((calcScore[0] == 2) &&
                 (calcScore[1] == 3) &&
                 (calcScore[2] == 4) &&
                 (calcScore[3] == 5) &&
                 (calcScore[4] == 6))) {
                Sum = 40;
            }
            Points = Sum;

            return Sum;
        }

        /// <summary>
        /// Checks for a Small Straight but checking for 
        /// 1,2,3,4  or 2,3,4,5  or 3,4,5,6, if this is true the score 
        /// value of 30 is given.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateSmallStraight(int[] calcScore) {
            int Sum = 0;
            
            // This for loop checks for any double up's of values. 
            for (int j = 0; j < 4; j++) {
                int temp = 0;
                if (calcScore[j] == calcScore[j + 1]) {
                    temp = calcScore[j];

                    for (int k = j; k < 4; k++) {
                        calcScore[k] = calcScore[k + 1];
                    }

                    calcScore[4] = temp;
                }
            }
 
            // There is 5 potential positions which means the begining and end must both be considered.
            if (((calcScore[0] == 1) && (calcScore[1] == 2) && (calcScore[2] == 3) && (calcScore[3] == 4)) ||
                ((calcScore[0] == 2) && (calcScore[1] == 3) && (calcScore[2] == 4) && (calcScore[3] == 5)) ||
                ((calcScore[0] == 3) && (calcScore[1] == 4) && (calcScore[2] == 5) && (calcScore[3] == 6)) ||
                ((calcScore[1] == 1) && (calcScore[2] == 2) && (calcScore[3] == 3) && (calcScore[4] == 4)) ||
                ((calcScore[1] == 2) && (calcScore[2] == 3) && (calcScore[3] == 4) && (calcScore[4] == 5)) ||
                ((calcScore[1] == 3) && (calcScore[2] == 4) && (calcScore[3] == 5) && (calcScore[4] == 6))) {
                Sum = 30;
            }
            Points = Sum;

            return Sum;
        }

        /// <summary>
        /// Checks if all the numbers are equal to each other then gives
        /// the score value of 50.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateYahtzee(int[] calcScore) {
            int Sum = 0;
            
            for (int i = 1; i <= 6; i++) {
                int Count = 0;
                for (int j = 0; j < 5; j++) { 
                    if (calcScore[j] == i)
                        Count++;

                    if (Count > 4)
                        Sum = 50;
                }
            }
            Points = Sum;

            return Sum;
        }

        

    }
}
