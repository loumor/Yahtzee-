using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// Form class contains the intialization of GUI controls and labels.
    /// </summary>
    public partial class Form1 : Form {

        private Label[] dice = new Label[5];
        private Button[] scoreButtons = new Button[(int)ScoreType.Yahtzee + 1];
        private Label[] scoreTotals = new Label[(int)ScoreType.GrandTotal + 1];
        private CheckBox[] checkBoxes = new CheckBox[5];
        private Game game;

        // Sets the number of players.
        private int numberofPlayers = 1;
        // Sets the maximum number of players.
        private const int MAX_PLAYERS = 6; 



        public Form1() {
            InitializeComponent();
            InInitializeLabelsAndButtons();
            DisableAndClearCheckBoxes();
            
        }
        

        private void InInitializeLabelsAndButtons() {

           
            // SECTION FOR BUTTONS.
            scoreButtons[(int)ScoreType.Ones] = button1;
            scoreButtons[(int)ScoreType.Twos] = button2;
            scoreButtons[(int)ScoreType.Threes] = button3;
            scoreButtons[(int)ScoreType.Fours] = button4;
            scoreButtons[(int)ScoreType.Fives] = button5;
            scoreButtons[(int)ScoreType.Sixes] = button6;
            scoreButtons[(int)ScoreType.ThreeOfAKind] = button7;
            scoreButtons[(int)ScoreType.FourOfAKind] = button8;
            scoreButtons[(int)ScoreType.FullHouse] = button9;
            scoreButtons[(int)ScoreType.SmallStraight] = button10;
            scoreButtons[(int)ScoreType.LargeStraight] = button11;
            scoreButtons[(int)ScoreType.Chance] = button12;
            scoreButtons[(int)ScoreType.Yahtzee] = button13;


            // SECTION FOR DICE LABEL.
            dice[0] = DieLabel1;  
            dice[1] = DieLabel2;
            dice[2] = DieLabel3;
            dice[3] = DieLabel4;
            dice[4] = DieLabel5;


            // SECTION FOR SCORE LABEL.
            scoreTotals[(int)ScoreType.Ones] = scoreLabel1;  
            scoreTotals[(int)ScoreType.Twos] = scoreLabel2;
            scoreTotals[(int)ScoreType.Threes] = scoreLabel3;
            scoreTotals[(int)ScoreType.Fours] = scoreLabel4;
            scoreTotals[(int)ScoreType.Fives] = scoreLabel5;
            scoreTotals[(int)ScoreType.Sixes] = scoreLabel6;
            scoreTotals[(int)ScoreType.ThreeOfAKind] = scoreLabel7;
            scoreTotals[(int)ScoreType.FourOfAKind] = scoreLabel8;
            scoreTotals[(int)ScoreType.FullHouse] = scoreLabel9;
            scoreTotals[(int)ScoreType.SmallStraight] = scoreLabel10;
            scoreTotals[(int)ScoreType.LargeStraight] = scoreLabel11;
            scoreTotals[(int)ScoreType.Chance] = scoreLabel12;
            scoreTotals[(int)ScoreType.Yahtzee] = scoreLabel13;
            scoreTotals[(int)ScoreType.SubTotal] = labelSubTotal;
            scoreTotals[(int)ScoreType.BonusFor63Plus] = labelBonus;
            scoreTotals[(int)ScoreType.SectionATotal] = labelUpperTotal;
            scoreTotals[(int)ScoreType.SectionBTotal] = labelLowerTotal;
            scoreTotals[(int)ScoreType.YahtzeeBonus] = labelYahzteeBonus;
            scoreTotals[(int)ScoreType.GrandTotal] = labelGrandTotal;

            // SECTION FOR CHECKBOXES.
             checkBoxes[0] = checkBox1;  
             checkBoxes[1] = checkBox2;
             checkBoxes[2] = checkBox3;
             checkBoxes[3] = checkBox4;
             checkBoxes[4] = checkBox5;
            
        }

        public Label[] GetDice() {

            return dice;
        }

        public Label[] GetScoreTotals() {
            
            return scoreTotals;  
        }

        public void ShowPlayerName(string name) {
            // Updates the player label to the value of 'string name'.
            playerLabel.Text = name; 
 
        }

        public void EnableRollButtons() {
            RollButton.Enabled = true; 
        }

        public void DisableRollButtons() {
            RollButton.Enabled = false; 

        }

        public void EnableCheckBoxes() {
            for (int i = 0; i < checkBoxes.Length; i++) {
                checkBoxes[i].Enabled = true;
            }

        }

        public void DisableAndClearCheckBoxes() {
            
            for (int i = 0; i < checkBoxes.Length; i++) {
                // Disables CheckBoxes.
                checkBoxes[i].Enabled = false;

                // Clears CheckBoxes.
                checkBoxes[i].Checked = false; 
            }
            
        }

        public void EnableScoreButton(ScoreType combo) {
         
           scoreButtons[(int)combo].Enabled = true;  
          
        }

        public void DisableScoreButton(ScoreType combo) {
            
            scoreButtons[(int)combo].Enabled = false; 
            

        }

        public void CheckCheckBox(int index) {
            
            checkBoxes[index].Checked = true; 
            
        }

        public void ShowMessage(string message) {
            messageLabel.Text = message; 
             
        }

        public void ShowOKButton() {
            okButton.Visible = true;
        }

        /// <summary>
        /// Makes the OK Button not visible. 
        /// </summary>
        public void HideOKButton() {
            okButton.Visible = false;
        }

        public void StartNewGame() {
            game = new Game(this);
            playerBindingSource.DataSource = game.Players;
            
        }
        
        /// <summary>
        /// Disables all the Score Buttons. 
        /// </summary>
        public void DisableAllButtons() { 

            for (int num = (int)ScoreType.Ones; num <= (int)ScoreType.Yahtzee; num++) {
                if (num < 6 || num > 8) {
                    scoreButtons[num].Enabled = false; 
                }

            }

        }

        /// <summary>
        /// Enables all the Score Buttons. 
        /// </summary>
        public void EnableAllButtons() {

            for (int num = (int)ScoreType.Ones; num <= (int)ScoreType.Yahtzee; num++) {
                if (num < 6 || num > 8) {

                    scoreButtons[num].Enabled = true; 
                }
            }

        }

        /// <summary>
        /// Disables the numeric up-down button. 
        /// </summary>
        public void DisableUpDown () {
            numericUpDown1.Enabled = false;
        }

        /// <summary>
        /// Enables the numeric up-down button.
        /// </summary>
        public void EnableUpDown () {
            numericUpDown1.Enabled = true;
        }

        private void UpdatePlayersDataGridView() {
            game.Players.ResetBindings();
        }

        /// <summary>
        /// Returns the number of players for the game.
        /// </summary>
        /// <returns></returns>
        public int numberofplayers() {
            return numberofPlayers;
        }

        /// <summary>
        /// Enables the save button.
        /// </summary>
        public void enablesave() {
            saveToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Disables the load button.
        /// </summary>
        public void disableload() {
            loadToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Clears all the score labels for the reset of a game. 
        /// </summary>
        public void clearScoreLabels() {
            for (int i = 0; i < (int)ScoreType.GrandTotal+1; i++) {
                scoreTotals[i].Text = "";
            }
        }

        /// <summary>
        /// Checks which labels are disabled and then disables the corresponding button.
        /// </summary>
        public void checkScoreButton() {
            for (int i = (int)ScoreType.Ones; i < (int)ScoreType.Yahtzee; i++) {
                if (scoreTotals[i].Enabled == false) {
                    scoreButtons[i].Enabled = false;
                }
            }
        }











       //EVENT HANDELERS 



        private void RollButton_Click(object sender, EventArgs e) {
            game.RollDice();

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            numericUpDown1.Value = 1;
            StartNewGame();
            numberofPlayers = 1;
            
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            // If checkbox[X] is checked. 
            if (checkBox1.Checked == true) {
                // Hold the value of its die on its label. 
                game.HoldDie(0); 
            }
            else {
                // Release if the checkbox[X] is not checked.
                game.ReleaseDie(0); 
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            if (checkBox2.Checked == true) {
                game.HoldDie(1);
            } else {
                game.ReleaseDie(1);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            if (checkBox3.Checked == true) {
                game.HoldDie(2);
            } else {
                game.ReleaseDie(2);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            if (checkBox4.Checked == true) {
                game.HoldDie(3);
            } else {
                game.ReleaseDie(3);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) {
            if (checkBox5.Checked == true) {
                game.HoldDie(4);
            } else {
                game.ReleaseDie(4);
            }
        }


        private void button1_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Ones combination - click OK button";
            // Outputs the message on the label.
            ShowMessage(newmessage);
            // Makes OK Button visible.
            ShowOKButton();
            // Passes the ScoreType to the Game class.
            game.ScoreCombination(ScoreType.Ones);
            // Disables Roll Buttons.
            DisableRollButtons();
            // Disables the specific Scorebutton for that player.
            game.IsNotAvailable(ScoreType.Ones);
            // Disables all the score buttons so that player can not score
            // more than once in their go.
            DisableAllButtons();
            // Updates players score on the Data Grid.
            UpdatePlayersDataGridView(); 

        }

        private void button2_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Twos combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            game.ScoreCombination(ScoreType.Twos);
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Twos);
            DisableAllButtons();
            UpdatePlayersDataGridView();

        }

        private void button3_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Threes combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Threes);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Threes);
            UpdatePlayersDataGridView();

        }

        private void button4_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Fours combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Fours);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Fours);
            UpdatePlayersDataGridView();

        }

        private void button5_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Fives combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Fives);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Fives);
            UpdatePlayersDataGridView();

        }

        private void button6_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Sixes combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Sixes);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Sixes);
            UpdatePlayersDataGridView();

        }

        private void button7_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen ThreeOfAKind combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.ThreeOfAKind);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.ThreeOfAKind);
            UpdatePlayersDataGridView();

        }

        private void button8_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen FourOfAKind combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.FourOfAKind);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.FourOfAKind);
            UpdatePlayersDataGridView();

        }

        private void button9_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen FullHouse combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.FullHouse);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.FullHouse);
            UpdatePlayersDataGridView();

        }

        private void button10_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen SmallStraight combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.SmallStraight);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.SmallStraight);
            UpdatePlayersDataGridView();

        }

        private void button11_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen LargeStraight combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.LargeStraight);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.LargeStraight);
            UpdatePlayersDataGridView();

        }

        private void button12_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Chance combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Chance);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Chance);
            UpdatePlayersDataGridView();

        }

        private void button13_Click(object sender, EventArgs e) {
            string newmessage = "You have choosen Yahtzee combination - click OK button";
            ShowMessage(newmessage);
            ShowOKButton();
            DisableRollButtons();
            game.IsNotAvailable(ScoreType.Yahtzee);
            DisableAllButtons();
            game.ScoreCombination(ScoreType.Yahtzee);
            UpdatePlayersDataGridView();

        }

        private void okButton_Click(object sender, EventArgs e) {
            ShowOKButton();
            clearScoreLabels();

            // If the game is over and the players wish to play another game.
            if (game.gameOverControls() == true) {  
                numericUpDown1.Value = 1;
                StartNewGame();
                numberofPlayers = 1;
            }
            else {
                game.NextTurn();
            }
        }

        /// <summary>
        /// Takes in the number of players then adds or removes the amount
        /// to the binding list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            
            if (numericUpDown1.Value > game.Players.Count) {
                game.addToList();
            }
            else {
                game.removeFromList();
            }
            
            numericUpDown1.Maximum = MAX_PLAYERS;
            numericUpDown1.Minimum = 1;
            numberofPlayers = (int)numericUpDown1.Value;
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            game.Save();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            game = Game.Load(this);
            playerBindingSource.DataSource = game.Players;
            UpdatePlayersDataGridView();

        }
    }

}

