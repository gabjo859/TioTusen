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

        public void PlayTurn(Player player) {
            IEnumerable<IDice> dices = Enumerable.Range(0, 6).Select(x => new Dice());
            dices = playerInputService.RollDices(player, dices);
            if (DiceCombinationAnalyzer.HasAnything(dices)) {
                var savedDices = playerInputService.GetSavedDices(player, dices);
                player.CurrentScore = DiceCombinationAnalyzer.Analyze(savedDices);
                if (player.TotalScore >= 1000 || player.CurrentScore  >= 1000) {
                    var stay = playerInputService.GetUserDecisionStay(player);
                    if (stay) {
                        player.TotalScore += player.CurrentScore;
                    }
                }
            }
        }
    }
}
