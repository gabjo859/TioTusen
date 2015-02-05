using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TenThousand.GameEngine.Interfaces;
using Rhino.Mocks;
using TenThousand.GameEngine;

namespace TenThousand.UnitTests {
    [TestClass]
    public class DiceCombinationAnalyzerTests {

        #region Tests

        [TestMethod]
        public void SingleDiceWithValueOneTest() {
            var dices = GenerateDices(new[] { 1 });
            Assert.AreEqual(100, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void SingleDiceWithValueFiveTest() {
            var dices = GenerateDices(new[] { 5 });
            Assert.AreEqual(50, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void TwoDiceWithValueFiveTest() {
            var dices = GenerateDices(new[] { 5, 5 });
            Assert.AreEqual(100, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void TwoDiceWithValueOneTest() {
            var dices = GenerateDices(new[] { 1, 1 });
            Assert.AreEqual(200, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void TwoDiceWithValueOneAndFiveTest() {
            var dices = GenerateDices(new[] { 1, 5 });
            Assert.AreEqual(150, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void ThreeDiceWithOnesAndFivesTest() {
            var dices = GenerateDices(new[] { 1, 1, 5 });
            Assert.AreEqual(250, DiceCombinationAnalyzer.Analyze(dices));

            dices = GenerateDices(new[] { 1, 5, 5 });
            Assert.AreEqual(200, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void FourDiceWithOnesAndFivesTest() {
            var dices = GenerateDices(new[] { 1, 1, 5, 5 });
            Assert.AreEqual(300, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void StraightGivesSixHundredTest() {
            var dices = GenerateDices(new[] { 1, 2, 3, 4, 5, 6 });
            Assert.AreEqual(600, DiceCombinationAnalyzer.Analyze(dices));
        }

        [TestMethod]
        public void ThreePairsGivesThreeHundredTest() {
            var dices = GenerateDices(new[] { 1, 1, 4, 4, 5, 5 });
            Assert.AreEqual(300, DiceCombinationAnalyzer.Analyze(dices));
            // TODO: This test is not extensive. Should somehow test all possible Three Pairs
        }

        [TestMethod]
        public void ThreeOrMoreDicesWithValueOneGivesThousandPointsTest() {

            // Three or more dices with value one gives a score of 1000*2^(numberOfDices-3)
            for (int numberOfDices = 3; numberOfDices <= 6; numberOfDices++) {
                List<int> diceValues = new List<int>();
                for (int i = 1; i <= numberOfDices; i++) {
                    diceValues.Add(1);
                }
                var dices = GenerateDices(diceValues.ToArray());
                var expectedScore = 1000 * Math.Pow(2, (numberOfDices - 3));
                Assert.AreEqual(expectedScore, DiceCombinationAnalyzer.Analyze(dices));
            }
        }

        /// <summary>
        /// Three or more of any value (except one) should give a score of (numberOfDices - 2) * 100 * diceValue
        /// </summary>
        [TestMethod]
        public void ThreeOrMoreOfAnyValueExceptOneTest() {
            
            // Test the cases of 3, 4, 5 and 6 dices of each dice value
            for (int numberOfDices = 3; numberOfDices <= 6; numberOfDices++) {

                // Test all dice values except ones
                for (int diceValue = 2; diceValue <= 6; diceValue++) {

                    List<int> diceValues = new List<int>();

                    // Generate dices of the current dice value and number of dices
                    for (int i = 1; i <= numberOfDices; i++) {
                        diceValues.Add(diceValue);
                    }
                    var dices = GenerateDices(diceValues.ToArray());

                    var expectedScore = (numberOfDices - 2) * 100 * diceValue;
                    Assert.AreEqual(expectedScore, DiceCombinationAnalyzer.Analyze(dices));
                }
            }
        }

        [TestMethod]
        public void HasNothingTest() {
            var dices = GenerateDices(new int[] { 2, 3, 3, 4, 6, 4 });
            Assert.IsFalse(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [TestMethod]
        public void HasAnythingASingleOneTest() {
            var dices = GenerateDices(new int[] { 2, 1, 3, 4, 6, 4 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [TestMethod]
        public void HasAnythingASingleFiveTest() {
            var dices = GenerateDices(new int[] { 2, 5, 3, 4, 6 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        [TestMethod]
        public void HasAnythingThreeOfSomethingTest() {
            var dices = GenerateDices(new int[] { 2, 2, 3, 4, 6, 2 });
            Assert.IsTrue(DiceCombinationAnalyzer.HasAnything(dices));
        }

        #endregion

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
