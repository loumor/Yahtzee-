using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// TotalOfDice class calculates the other Lower section totals 
    /// and returns their value.
    /// </summary>
    [Serializable]
    class TotalOfDice : Combination {
        private int numberOfOneKind = 0;

        public TotalOfDice(ScoreType score, Label combo) : base (combo) {

            // Sets the variable based on the ScoreType button that has been clicked.
            // Sets the variable equal to the number that each method requires. 

            //Three Of A Kind.
            if ((int)score == 9) { 
                numberOfOneKind = 2;
            }
            //Four Of A Kind.
            if ((int)score == 10) { 
                numberOfOneKind = 3;
            }
            //Chance.
            if ((int)score == 14) {  
                numberOfOneKind = 0;
            }



        } 

        public override int CalculateScore(int[] calcScore){
            // Checks each ScoreType and runs the correct method.
            if (numberOfOneKind == 2) {
                return CalculateThreeOfAKind(calcScore);
            }

            if (numberOfOneKind == 3) {
                return CalculateFourOfAKind(calcScore);
            }

            if (numberOfOneKind == 0) {
                return AddUpChance(calcScore);
            }
            else {
                return 0;
            }


        }

        /// <summary>
        /// Calculates the Three Of A Kind score by checking that 
        /// at least 3 or more of the numbers are equal to each other.
        /// If true a score of all the dice added together is returned. 
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateThreeOfAKind(int[] calcScore) {
            int Sum = 0;
            bool ThreeOfAKind = false;

            for (int i = 1; i <= 6; i++) {
                int Count = 0;
                // Implements a search and compare algorithm.
                for (int j = 0; j < 5; j++) { 
                    if (calcScore[j] == i)
                        Count++;

                    if (Count > numberOfOneKind) {
                        ThreeOfAKind = true;
                    }
                }
            }

            if (ThreeOfAKind) {
                for (int k = 0; k < 5; k++) {
                    Sum += calcScore[k];
                }
            }
            Points = Sum;

            return Sum;
        }


        /// <summary>
        /// Calculates the Four Of A Kind score by checking that 
        /// at least 4 or more of the numbers are equal to each other.
        /// If true a score of all the dice added together is returned.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int CalculateFourOfAKind(int[] calcScore) {
            int Sum = 0;
            bool FourOfAKind = false;

            for (int i = 1; i <= 6; i++) {
                int Count = 0;
                // Implements a search and compare algorithm.
                for (int j = 0; j < 5; j++) {
                    if (calcScore[j] == i)
                        Count++;

                    if (Count > numberOfOneKind) {
                        FourOfAKind = true;
                    }
                }
            }

            if (FourOfAKind) {
                for (int k = 0; k < 5; k++) {
                    Sum += calcScore[k];
                }
            }
            Points = Sum;

            return Sum;
        }

        /// <summary>
        /// Adds all the values of the dice together and returns the total.
        /// </summary>
        /// <param name="calcScore"></param>
        /// <returns></returns>
        public int AddUpChance(int[] calcScore) {
            int Sum = 0;
            
                for (int i = 0; i < 5; i++) { 
                    Sum += calcScore[i];
                }

            Points = Sum;

            return Sum;
        }



    }
}
