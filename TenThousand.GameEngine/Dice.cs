using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.GameEngine {
    public class Dice : IDice {

        private Random random;

        public Dice() {
            this.IsRoled = false;
            this.random = new Random();
        }

        #region Properties

        public void Role() {
            this.diceValue = random.Next(1, 7);
            this.IsRoled = true;
        }

        public bool IsRoled { get; private set; }

        private int diceValue;
        public int Value {
            get {
                if (this.IsRoled == false) {
                    this.Role();
                }
                return this.diceValue;
            }
            private set {
                this.diceValue = value;
            }
        }

        #endregion
    }
}
