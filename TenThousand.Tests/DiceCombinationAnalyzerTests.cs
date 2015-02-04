using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TenThousand.GameEngine;
using TenThousand.GameEngine.Interfaces;
using Rhino.Mocks;
using System.Collections;

namespace TenThousand.Tests {
    [TestFixture]
    public class DiceCombinationAnalyzerTests {

        #region Tests

        [Test(Description = "A single dice with value one should give a score of 100")]
        public void SingleDiceWithValueOneTest() {
            var dices = GenerateDices(new[] { 1 });
            Assert.AreEqual(100, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test(Description = "A single dice with value five should give a score of 50")]
        public void SingleDiceWithValueFiveTest() {
            var dices = GenerateDices(new[] { 5 });
            Assert.AreEqual(50, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void TwoDiceWithValueFiveTest() {
            var dices = GenerateDices(new[] { 5, 5 });
            Assert.AreEqual(100, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void TwoDiceWithValueOneTest() {
            var dices = GenerateDices(new[] { 1, 1 });
            Assert.AreEqual(200, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void TwoDiceWithValueOneAndFiveTest() {
            var dices = GenerateDices(new[] { 1, 5 });
            Assert.AreEqual(150, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void ThreeDiceWithOnesAndFivesTest() {
            var dices = GenerateDices(new[] { 1, 1, 5 });
            Assert.AreEqual(250, DiceCombinationAnalyzer.Analyze(dices));

            dices = GenerateDices(new[] { 1, 5, 5 });
            Assert.AreEqual(200, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void FourDiceWithOnesAndFivesTest() {
            var dices = GenerateDices(new[] { 1, 1, 5, 5 });
            Assert.AreEqual(300, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test(Description = "Six dices with a straight from 1-6 should give a score of 600")]
        public void StraightGivesSixHundredTest() {
            var dices = GenerateDices(new[] { 1, 2, 3, 4, 5, 6 });
            Assert.AreEqual(600, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test(Description = "Three pairs should give a score of 300")]
        public void ThreePairsGivesThreeHundredTest() {
            var dices = GenerateDices(new[] { 1, 1, 4, 4, 5, 5 });
            Assert.AreEqual(300, DiceCombinationAnalyzer.Analyze(dices));
            // TODO: This test is not extensive. Should somehow test all possible Three Pairs
        }

        [Test(Description = "Three or more dices with value one gives a score of 1000*2^(numberOfDices-3)")]
        public void ThreeOrMoreDicesWithValueOneGivesThousandPointsTest([Values(3,4,5,6)] int numberOfDices) {

            List<int> diceValues = new List<int>();
            for (int i = 1; i <= numberOfDices; i++) {
                diceValues.Add(1);
            }
            var dices = GenerateDices(diceValues.ToArray());
            var expectedScore = 1000 * Math.Pow(2, (numberOfDices - 3));
            Assert.AreEqual(expectedScore, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test(Description = "Three or more of any value (except one) should give a score of (numberOfDices - 2) * 100 * diceValue")]
        public void ThreeOrMoreOfAnyValueExceptOneTest(
            [Values(2, 3, 4, 5, 6)] int diceValue,
            [Values(3, 4, 5, 6)] int numberOfDices) {

            List<int> diceValues = new List<int>();
            for (int i = 1; i <= numberOfDices; i++) {
                diceValues.Add(diceValue);
            }
            var dices = GenerateDices(diceValues.ToArray());

            var expectedScore = (numberOfDices - 2) * 100 * diceValue;
            Assert.AreEqual(expectedScore, DiceCombinationAnalyzer.Analyze(dices));
        }

        [Test]
        public void HasNothingTest() {
            var dices = GenerateDices(new int[] { 2, 3, 3, 4, 6, 4});
            Assert.IsFalse(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [Test]
        public void HasAnythingASingleOneTest() {
            var dices = GenerateDices(new int[] { 2, 1, 3, 4, 6, 4 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [Test]
        public void HasAnythingASingleFiveTest() {
            var dices = GenerateDices(new int[] { 2, 5, 3, 4, 6 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [Test]
        public void HasAnythingThreeOfSomethingTest() {
            var dices = GenerateDices(new int[] { 2, 2, 3, 4, 6, 2 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        #endregion //Tests

        #region Helpers

        private IEnumerable<IDice> GenerateDices(int[] values) {
            foreach (var value in values) {
                var dice = MockRepository.GenerateMock<IDice>();
                dice.Stub(d => d.Value).Return(value);
                yield return dice;
            }
        }

        #endregion
    }
}
