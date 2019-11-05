using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// Player class decides the score combination classes for each scoring type 
    /// also updating the GUI for the specific player. 
    /// </summary>
    [Serializable]
    public class Player {
        private string name;
        private int combinationsToDo = 13;
        private Score[] scores = new Score[(int)ScoreType.GrandTotal + 1]; 
        private int grandTotal;
        

        public Player (string name, Label[] scoreTOTALS) {
            this.name = name;

            // For loop will run through all the 18 ScoreType's to set the array scores[] to a value.
            for (ScoreType combo = ScoreType.Ones; combo <= ScoreType.GrandTotal; combo++) {
                switch (combo) {
                    // For the ScoreType of:
                    case ScoreType.Ones:
                    case ScoreType.Twos:
                    case ScoreType.Threes:
                    case ScoreType.Fours:
                    case ScoreType.Fives:
                    case ScoreType.Sixes: {
                            // Instantains the scores[] array to it corresponding label and passes to its specific score class.
                            scores[(int)combo] = new CountingCombination(combo, scoreTOTALS[(int)combo]);
                            
                            break;
                        }
                    // For the ScoreType of:
                    case ScoreType.ThreeOfAKind:
                    case ScoreType.FourOfAKind:
                    case ScoreType.Chance: {

                            scores[(int)combo] = new TotalOfDice(combo, scoreTOTALS[(int)combo]);

                            break;
                        }
                    // For the ScoreType of:
                    case ScoreType.FullHouse:
                    case ScoreType.SmallStraight:
                    case ScoreType.LargeStraight:
                    case ScoreType.Yahtzee: {

                            scores[(int)combo] = new FixedScore(combo, scoreTOTALS[(int)combo]);

                            break;
                        }

                    case ScoreType.SubTotal:
                    case ScoreType.SectionATotal:
                    case ScoreType.SectionBTotal:
                    case ScoreType.BonusFor63Plus:
                    case ScoreType.YahtzeeBonus:
                    case ScoreType.GrandTotal: {

                            scores[(int)combo] = new BonusOrTotal(scoreTOTALS[(int)combo]);

                            break;

                        }
 
                }
            }

        }

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        public void ScoreCombination(int[] combination, ScoreType scoreCombo) {
            // Passes the scores array of a specific type denoted by the passed ScoreType
            // to the Combination Class where the abstract method CalculateScore takes in
            // the array combination of the 5 die facevalues after it has been sorted.
            ((Combination)scores[(int)scoreCombo]).Sort(combination);

            // Calculates the score for the SubTotal Label by adding together 
            // the Ones, Twos, Threes, Fours, Fives, Sixes points.
            scores[(int)ScoreType.SubTotal].Points = (scores[0].Points + scores[1].Points + scores[2].Points + scores[3].Points + scores[4].Points + scores[5].Points);
            
            // Places the bonus of 35 points into the Bonus Label if 
            // the SubTotal label is equal to or above 63 points.
            if(scores[(int)ScoreType.SubTotal].Points >= 63) {
                scores[(int)ScoreType.BonusFor63Plus].Points = 35;
            }

            // Calculates the Upper Total by adding together the Sub Total 
            // and the Bonus label.
            scores[(int)ScoreType.SectionATotal].Points = scores[(int)ScoreType.SubTotal].Points + scores[(int)ScoreType.BonusFor63Plus].Points;
            combinationsToDo--; 
            
            // Calculates the Yahtzee Bonus by checking to see the value of 
            // yahtzee label is equal to two thrown yahtzee's (100 points).
            if (scores[(int)ScoreType.Yahtzee].Points == 100) {
                scores[(int)ScoreType.YahtzeeBonus].Points = 100;
            }

            // Calculates the score for the Lower Total Label by adding together 
            // Three Of A Kind, Four Of A Kind, Full House, Small Straight, Large Straight,
            // Chance, Yahtzee and Yahtzee Bonus points.
            scores[(int)ScoreType.SectionBTotal].Points = (scores[9].Points + scores[10].Points + scores[11].Points + scores[12].Points + scores[13].Points + scores[14].Points + scores[15].Points + scores[16].Points);

            // Calculates the GrandTotal by adding together the Upper and Lower Total
            scores[(int)ScoreType.GrandTotal].Points = (scores[(int)ScoreType.SectionBTotal].Points + scores[(int)ScoreType.SectionATotal].Points);
            grandTotal = scores[(int)ScoreType.GrandTotal].Points;
        }


        public int GrandTotal {
            get { return grandTotal; }

        }

        public bool IsAvailable (ScoreType done) {
             // Makes the label for that score not enabled.
            scores[(int)done].notAvailable(); 

            return true;
        } 

        public void ShowScores() {

            for (int i = (int)ScoreType.Ones; i < (int)ScoreType.GrandTotal; i++) {
                // Updates all the score labels to their points values.
                scores[i].ShowScore(); 
            }
            
        }

        public bool IsFinished() {
            if (combinationsToDo == 0) {
                return true;
            }
            else {
                return false; 
            }
        }

        public void Load(Label[] scoreTotals) {
            for (int i = 0; i < scores.Length; i++) {
                scores[i].Load(scoreTotals[i]);
            }
        }//end Load

        

       


    }


}
