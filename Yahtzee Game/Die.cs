using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Yahtzee_Game {
    /// <summary>
    /// Die class runs the die logic, creating and rolling each die.
    /// </summary>
    [Serializable]
    public class Die {

        private int faceValue;
        private bool active;
        [NonSerialized]
        private Label label;
        private static Random random = new Random();
        private static bool DEBUG = false;
        private static string rollFileName = Game.defaultPath + "\\basictestrolls.txt";
        [NonSerialized]
        private static StreamReader rollFile = new StreamReader(rollFileName);
        // Gives FaceValue a value before its Rolled.
        const int DEFAULT_FACE_VALUE = 1; 
        
        public Die(Label Dielabel) {
            label = Dielabel;
            active = true;
            faceValue = DEFAULT_FACE_VALUE;
            
        }

        public int FaceValue {
            get { return faceValue; }
        }

        public bool Active {
            get {
                return active;
            }
            set {
                active = value;
            }
        }

        public void Roll() {
            if (!DEBUG) {
                // Only if the Die is Active will it roll.
                if (Active == true) { 
                
                faceValue = random.Next(1, 7);
            }
            } else {
                faceValue = int.Parse(rollFile.ReadLine());
            }
            label.Text = faceValue.ToString();
            label.Refresh(); 
            
        }

        public void Load(Label label) {
            this.label = label;
            if (faceValue == 0) {
                label.Text = string.Empty;
            } else {
                label.Text = faceValue.ToString();
            }
        }//end Load


    }
}
