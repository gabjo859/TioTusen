using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TenThousand.GameEngine.Entities;

namespace TenThousand.GameEngine.Interfaces {
    public interface IPlayerInputService {

        /// <summary>
        /// Let the player roll a set of dices.
        /// </summary>
        /// <param name="player">The player who should roll dices</param>
        /// <param name="dices">The dices to be rolled</param>
        /// <returns>The rolled dices</returns>
        IEnumerable<IDice> RollDices(Player player, IEnumerable<IDice> dices);

        /// <summary>
        /// Ask the player to choose one or more dices to save out of a given set of rolled dices.
        /// </summary>
        /// <param name="player">The player to make the choice</param>
        /// <param name="dices">A set of rolled dices to choose from</param>
        /// <returns>The dices chosen</returns>
        IEnumerable<IDice> GetSavedDices(Player player, IEnumerable<IDice> dices);

        /// <summary>
        /// Ask the user to stay or continue rolling dices.
        /// </summary>
        /// <param name="player">The player to make the decision</param>
        /// <returns>True if the user stays, false if the player wants to continue the turn</returns>
        bool GetUserDecisionStay(Player player);
    }
}
