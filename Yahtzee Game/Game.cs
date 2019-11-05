using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yahtzee_Game {

    public enum ScoreType {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        SubTotal, BonusFor63Plus, SectionATotal,
        ThreeOfAKind, FourOfAKind, FullHouse,
        SmallStraight, LargeStraight, Chance, Yahtzee,
        YahtzeeBonus, SectionBTotal, GrandTotal
    }
    /// <summary>
    /// Game class runs the logic of the game.
    /// </summary>
    [Serializable]
    public class Game {

        private BindingList<Player> players = new BindingList<Player>();
        private int currentPlayerindex;
        private Player currentPlayer;
        private Die[] dice = new Die[5]; 
        private int playersFinished;
        private int numRolls;
        [NonSerialized]
        private Form1 form;
        [NonSerialized]
        private Label[] dieLabels = new Label[5];

        /// <summary>
        /// Creates an array of generic player names.
        /// </summary>
       private string[] Names = { "Player One", "Player Two", "Player Three", "Player Four", "Player Five", "Player Six" };

       public static string defaultPath = Environment.CurrentDirectory;
       private static string savedGameFile = defaultPath + "\\YahtzeeGame.dat"; 



        public BindingList<Player> Players {
            get {
                return players;
            }
        }

        /// <summary>
        /// Adds players to the binding list.
        /// </summary>
        public void addToList() {
                players.Add(new Player(Names[form.numberofplayers()], form.GetScoreTotals()));
        }

        /// <summary>
        /// Removes players from the binding list.
        /// </summary>
        public void removeFromList() {
            players.Remove(players[form.numberofplayers()-1]);
        }


        /// <summary>
        /// Creates a new game and initiates the GUI desgin and starting buttons.
        /// </summary>
        /// <param name="form"></param>
        public Game(Form1 form) {
            form.clearScoreLabels();
            players.ResetBindings();
            players.Add(new Player(Names[0], form.GetScoreTotals()));

            string message = "Please Select the Number of Players";
            currentPlayerindex = 0;
            playersFinished = 0;
            numRolls = 0;

            this.form = form;
            this.dieLabels = this.form.GetDice();
         
            for (int i=0; i < dice.Length; i++) {
                dice[i] = new Die(dieLabels[i]);
            }
            currentPlayer = players[currentPlayerindex];


            for (int i = 0; i < dice.Length; i++) {
                dieLabels[i].Text = "";
            }

            string name = Names[currentPlayerindex];
            form.DisableAndClearCheckBoxes();
            form.DisableAllButtons();
            form.ShowPlayerName(name);
            form.EnableUpDown();
            form.EnableRollButtons();
            form.ShowMessage(message);
            form.HideOKButton();
            form.enablesave();
            form.disableload();
        
        }

        
        public void NextTurn() {

            if (currentPlayer.IsFinished() == true) {
                // Runs a method that sets the Game Over GUI.
                gameOverControls(); 
            }

            // If the current player index is larger than the 
            // number of players on the binding list it loops back to 0.
            if (currentPlayerindex > form.numberofplayers() - 2) {  
                currentPlayerindex = 0; 
            }                            
            else {                      
                currentPlayerindex++;
            }

            currentPlayer = players[currentPlayerindex];
            string name = Names[currentPlayerindex];
            
            playersFinished++;

            string message = "Roll 1";

            for (int i = 0; i < dice.Length; i++) {
                dieLabels[i].Text = "";
            }
            
            numRolls = 0;
            currentPlayer.ShowScores(); 
            form.DisableAllButtons();
            form.DisableAndClearCheckBoxes();
            form.EnableRollButtons();
            form.ShowPlayerName(name);
            form.ShowMessage(message);
            form.HideOKButton();

        }

        public void RollDice() {
            //Once the first roll has been made no more players can be added.
            form.DisableUpDown();
            for (int i = 0; i < dice.Length; i++) {
                
                dice[i].Roll();
                
                dieLabels[i].Text = dice[i].FaceValue.ToString();
            }
            
            if (numRolls == 0) {
                string message = "Roll 1";
                form.EnableAllButtons();
                // Turns off the already used scorebuttons.
                form.checkScoreButton(); 
                form.EnableCheckBoxes();
                form.ShowMessage(message);
            }

            if (numRolls == 1) {
                string message = "Roll 2 or choose a combination to score";
                form.ShowMessage(message);  
            }

            if (numRolls == 2) {
                string message = "Roll 3, Your turn has ended - click OK";
                form.ShowMessage(message);
                form.DisableRollButtons();
                form.ShowOKButton();
            }

            numRolls++;

        }

        public void HoldDie(int hold) {
            dice[hold].Active = false;
        }
        public void ReleaseDie(int release) {
            dice[release].Active = true;
        }
  
        public void ScoreCombination(ScoreType SCORE) {
            int[] dicess = new int[5];

            for (int i = 0; i < dice.Length; i++) {
                 dicess[i] = dice[i].FaceValue;
            }
            // Passes dice array and the specific scorebuttn to player class.
            currentPlayer.ScoreCombination(dicess, SCORE);  
            
            form.ShowOKButton();
        }

        /// <summary>
        /// Passes the clicked ScoreType to the method in Player,
        /// disabeling the label for this players future turns.
        /// Then if the label is disabled it returns the ScoreType that
        /// then disabels the Button corresponding to label.
        /// </summary>
        /// <param name="done"></param>
        /// <returns></returns>
        public void IsNotAvailable(ScoreType done) {
            if (currentPlayer.IsAvailable(done) == true) {
                form.DisableScoreButton(done);
            }

        }
        
        /// <summary>
        /// Sets the GUI controls for a Game Over. 
        /// </summary>
        public bool gameOverControls() {
            string message = "The Winner is " + WinnerName() + " With a Socre of " + WinnerScore() +  " Would You Like To Play Another Game?";
            if (currentPlayer.IsFinished() == true) {
                MessageBox.Show(message);
                form.ShowOKButton();
                return true;
            } else {
                return false;
            }
            
        }

        /// <summary>
        /// Searchs for the largest score. 
        /// </summary>
        /// <returns></returns>
        public int WinnerScore() {
            int max = int.MinValue;
            foreach (Player type in players) {
                if (type.GrandTotal > max) {
                    max = type.GrandTotal;
                }
            }
            return max;
        }

        /// <summary>
        /// Searches for the Player who has the largest score.
        /// </summary>
        /// <returns></returns>
        public string WinnerName() {
            string name = "";
            int max = WinnerScore();

            foreach (Player type in players) {
                if (type.GrandTotal == max) {
                    name = type.Name;
                }
            }
            return name;
        }







        /// <summary>
        /// Load a saved game from the default save game file
        /// </summary>
        /// <param name="form">the GUI form</param>
        /// <returns>the saved game</returns>
        public static Game Load(Form1 form) {
            Game game = null;
            if (File.Exists(savedGameFile)) {
                try {
                    Stream bStream = File.Open(savedGameFile, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    game = (Game)bFormatter.Deserialize(bStream);
                    bStream.Close();
                    game.form = form;
                    game.ContinueGame();
                    return game;
                } catch {
                    MessageBox.Show("Error reading saved game file.\nCannot load saved game.");
                }
            } else {
                MessageBox.Show("No current saved game.");
            }
            return null;
        }


        /// <summary>
        /// Save the current game to the default save file
        /// </summary>
        public void Save() {
            try {
                Stream bStream = File.Open(savedGameFile, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(bStream, this);
                bStream.Close();
                MessageBox.Show("Game saved");
            } catch (Exception e) {

                //   MessageBox.Show(e.ToString());
                MessageBox.Show("Error saving game.\nNo game saved.");
            }
        }

        /// <summary>
        /// Continue the game after loading a saved game
        /// 
        /// Assumes game was saved at the start of a player's turn before they had rolled dice.
        /// </summary>
        private void ContinueGame() {
            LoadLabels(form);
            for (int i = 0; i < dice.Length; i++) {
                //uncomment one of the following depending how you implmented Active of Die
                // dice[i].SetActive(true);
                // dice[i].Active = true;
            }

            form.ShowPlayerName(currentPlayer.Name);
            form.EnableRollButtons();
            form.EnableCheckBoxes();
            // can replace string with whatever you used
            form.ShowMessage("Roll 1");
            currentPlayer.ShowScores();
        }//end ContinueGame

        /// <summary>
        /// Link the labels on the GUI form to the dice and players
        /// </summary>
        /// <param name="form"></param>
        private void LoadLabels(Form1 form) {
            Label[] diceLabels = form.GetDice();
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Load(diceLabels[i]);
            }
            for (int i = 0; i < players.Count; i++) {
                players[i].Load(form.GetScoreTotals());
            }

        }







    }
}
