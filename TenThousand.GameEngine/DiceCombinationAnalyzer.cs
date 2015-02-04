using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.GameEngine {
    public static class DiceCombinationAnalyzer {

        /// <summary>
        /// Analyze and calculate the score of a set of dices.
        /// </summary>
        /// <param name="dices">A set of dices</param>
        /// <returns>The total score of the dices</returns>
        public static int Analyze(IEnumerable<IDice> dices) {
            var score = 0;

            // Do we have anything at all?
            if (!HasAnything(dices)) { return score; }

            // Three Pairs or Straight?
            if (dices.Count() == 6) {
                if (CheckIfThreePairs(dices)) { return 300; }
                if (CheckIfStraight(dices)) { return 600; }
            }

            var groupedDices = dices.GroupBy(x => x.Value);

            // There should not be any pairs or single values (except ones or fives)
            if (groupedDices.Where(g => g.Key != 1 && g.Key != 5)
                .Any(g => g.Count() < 3)) {
                return score;
            }

            // Add score for three or more dices with same value
            var threeOrMore = groupedDices.Where(g => g.Count() >= 3);
            foreach (var g in threeOrMore) {
                score += GetScoreForThreeOrMore(g.Key, g.Count());
            }

            // Add single or double ones
            score += GetScoreForSingleAndDubles(groupedDices, 1);

            // Add score for single or double fives
            score += GetScoreForSingleAndDubles(groupedDices, 5);
            
            return score;
        }

        private static int GetScoreForSingleAndDubles(IEnumerable<IGrouping<int, IDice>> diceGroups, int key) {
            int multiplier = key == 1 ? 100 : 50;
            int score = 0;
            if (diceGroups.Any(g => g.Key == key)) {
                var numberOfdices = diceGroups
                    .Where(g => g.Key == key)
                    .SingleOrDefault()
                    .Count();
                if (numberOfdices < 3) {
                    score = numberOfdices * multiplier;
                }
            }
            return score;
        }

        private static int GetScoreForThreeOrMore(int value, int count) {
            if (count < 3) return 0;
            if (value == 1) {
                return 1000 * (int)Math.Pow(2, (count - 3));
            } else {
                return (count - 2) * 100 * value;
            }
        }

        private static bool CheckIfStraight(IEnumerable<IDice> dices) {
            if (dices.Count() != 6) { return false; }
            var groupedValues = dices.GroupBy(d => d.Value);
            return groupedValues.All(g => g.Count() == 1);
        }

        private static bool CheckIfThreePairs(IEnumerable<IDice> dices) {
            if (dices.Count() != 6) { return false; }
            var groupedValues = dices.GroupBy(d => d.Value);
            return groupedValues.All(g => g.Count() == 2);
        }


        public static bool HasAnything(IEnumerable<IDice> dices) {
            if (CheckIfStraight(dices) || CheckIfThreePairs(dices)) { return true; }

            var groupedValues = dices.GroupBy(d => d.Value);
            var threeOfSomething = groupedValues.Any(g => g.Count() >= 3);
            var onesOrFives = groupedValues.Where(g => g.Key == 1 || g.Key == 5).Count() > 0;

            return threeOfSomething || onesOrFives;
        }
    }
}
