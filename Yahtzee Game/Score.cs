using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {
    /// <summary>
    /// Score class updates the label text on the GUI by returning the points 
    /// values of the calculated score combinations.
    /// </summary>
    [Serializable]
    public abstract class Score {
        private int points = 0;
        [NonSerialized]
        private Label label;
        protected bool done = false;
        
        public Score(Label label) {
            this.label = label; 

        }

        public int Points {
            get { return points; }

            set {
                points = value;
                done = true;
                label.Text = points.ToString();
            }
        }

        public bool Done {
            get { return done; }

        }

        public void ShowScore(){
            if (done) {
                label.Text = points.ToString();
            }
            else {
                label.Text = "";
            }

        }

        public void Load(Label label) {
            this.label = label;
            
        } //end Load

        /// <summary>
        /// Makes the label not enabled
        /// </summary>
        public void notAvailable() {
            label.Enabled = false;
        }

    }
}
