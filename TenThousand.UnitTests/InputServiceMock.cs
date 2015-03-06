using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenThousand.GameEngine.Entities;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.UnitTests {
    public class InputServiceMock : IPlayerInputService {

        public int GetSavedDicesCalled = 0;
        public int RollDicesCalled = 0;
        public int GetUserDecisionCalled = 0;

        public Func<Player, IEnumerable<IDice>, IEnumerable<IDice>> RollDicesMock { get; set; }
        public Func<Player, IEnumerable<IDice>, IEnumerable<IDice>> GetSavedDicesMock { get; set; }
        public Func<Player, bool> GetUserDecisionStayMock { get; set; }


        public IEnumerable<IDice> RollDices(Player player, IEnumerable<IDice> dices) {
            RollDicesCalled++;
            if (RollDicesMock != null) { 
                return RollDicesMock(player, dices); 
            }
            foreach (var dice in dices) {
                dice.Role();
            }
            return dices;
        }
        
        public IEnumerable<IDice> GetSavedDices(Player player, IEnumerable<IDice> dices) {
            GetSavedDicesCalled++;
            if (GetSavedDicesMock != null) {
                return GetSavedDicesMock(player, dices);
            }
            return dices.Where(x => x.Value == 1 || x.Value == 5);
        }

        public bool GetUserDecisionStay(Player player) {
            GetUserDecisionCalled++;
            if (GetUserDecisionStayMock != null) {
                return GetUserDecisionStayMock(player);
            }
            return player.CurrentScore >= 300;
        }
    }
}
