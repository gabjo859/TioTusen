using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenThousand.GameEngine.Entities {
    public class Game {

        public IEnumerable<Player> Players { get; set; }

        public Player CurrentPlayer { get; set; }
    }
}
