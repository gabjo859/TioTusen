using System;
namespace TenThousand.GameEngine.Interfaces {
    /// <summary>
    /// This interface describes the properties of a Dice and what Properties that needs to be implemented.
    /// </summary>
    public interface IDice {
        /// <summary>
        /// Indicates if the dice has been roled or not and therefore could be expected to have a valid value.
        /// </summary>
        bool IsRoled { get; }
        /// <summary>
        /// Performes a role of the dice which should give the dice a new random value between 1-6.
        /// </summary>
        void Role();
        /// <summary>
        /// Represents the current value of the dice, corresponding to the side facing up on a regular dice.
        /// Should return a value between 1-6.
        /// </summary>
        int Value { get; }
    }
}
