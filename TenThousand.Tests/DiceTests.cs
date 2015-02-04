using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TenThousand.GameEngine;

namespace TenThousand.Tests {
    [TestFixture]
    public class DiceTests {

        [Test]
        public void DiceMustBeRoledToHaveValueTest() {
            var dice = new Dice();
            Assert.IsFalse(dice.IsRoled);
        }

        [Test]
        public void DiceHasValueAfterRole() {
            var dice = new Dice();
            dice.Role();
            Assert.IsTrue(dice.IsRoled);
        }

        [Test]
        public void MaxValueIsSixTest() {
            var dice = new Dice();
            int maxValue = 0;

            for (int i = 0; i < 1000; i++) {
                dice.Role();
                if (dice.Value > maxValue) { maxValue = dice.Value; }
            }
            Assert.AreEqual(6, maxValue);
        }

        [Test]
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
