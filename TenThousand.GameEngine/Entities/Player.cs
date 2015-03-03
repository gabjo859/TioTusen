using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.GameEngine.Entities {
    public class Player {

        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total score in the current game
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// The current score during one turn
        /// </summary>
        public int CurrentScore { get; set; }
    }
}
