using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TenThousand.GameEngine.Entities;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.GameEngine {
    public class GameService {

        private IPlayerInputService playerInputService;

        public GameService(IPlayerInputService playerInputService) {
            this.playerInputService = playerInputService;
        }

        public void playTurn(Player player) {

        }
    }
}
