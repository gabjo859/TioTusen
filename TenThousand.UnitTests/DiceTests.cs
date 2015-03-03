using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TenThousand.GameEngine.Entities;

namespace TenThousand.UnitTests {
    [TestClass]
    public class DiceTests {
        [TestMethod]
        public void DiceMustBeRoledToHaveValueTest() {
            var dice = new Dice();
            Assert.IsFalse(dice.IsRoled);
        }

        [TestMethod]
        public void DiceHasValueAfterRole() {
            var dice = new Dice();
            dice.Role();
            Assert.IsTrue(dice.IsRoled);
        }

        [TestMethod]
        public void MaxValueIsSixTest() {
            var dice = new Dice();
            int maxValue = 0;

            for (int i = 0; i < 1000; i++) {
                dice.Role();
                if (dice.Value > maxValue) { maxValue = dice.Value; }
            }
            Assert.AreEqual(6, maxValue);
        }

        [TestMethod]
        public void MinValueIsOneTest() {
            var dice = new Dice();
            int minValue = 6;

            for (int i = 0; i < 1000; i++) {
                dice.Role();
                if (dice.Value < minValue) { minValue = dice.Value; }
            }
            Assert.AreEqual(1, minValue);
        }
    }
}
