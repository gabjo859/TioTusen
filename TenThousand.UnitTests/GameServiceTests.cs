using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TenThousand.GameEngine.Entities;
using Rhino.Mocks;
using TenThousand.GameEngine.Interfaces;
using TenThousand.GameEngine;
using System.Collections.Generic;
using System.Linq;

namespace TenThousand.UnitTests {
    [TestClass]
    public class GameServiceTests {
        [TestMethod]
        public void PlayTurn_DicesShouldBeRolledByPlayerTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);
            
            Assert.AreNotEqual(0, inputService.RollDicesCalled, "Number of calls to RollDices");
        }

        [TestMethod]
        public void PlayTurn_SixDicesShouldBeRolledTheFirstTimeTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => {
                Assert.AreEqual(6, d.Count(), "Number of dices rolled first time");
                return d;
            };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);
        }

        [TestMethod]
        public void PlayTurn_PlayerGetsNothing_ShouldNotBeAskedToSaveDicesTest() {
            var player = new Player();          
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => { return TestUtils.GenerateDices(new[] { 2, 3, 3, 4, 6, 6 }); };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreEqual(1, inputService.RollDicesCalled, "Number of calls to RollDices");
            Assert.AreEqual(0, inputService.GetSavedDicesCalled, "Number of calls to GetSavedDices");
        }

        [TestMethod]
        public void PlayTurn_PlayerGetsSomething_ShouldBeAskedToSaveDicesTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => {
                return TestUtils.GenerateDices(new[] { 1, 1, 5, 3, 4, 6 });
            };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreNotEqual(0, inputService.GetSavedDicesCalled, "Number of calls to GetSavedDices");
        }

        [TestMethod]
        public void PlayTurn_PlayerIsNotInGameAndGetsThousand_ShouldBeAskedToStayTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => {
                return TestUtils.GenerateDices(new[] { 1, 1, 1, 3, 4, 6 });
            };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreNotEqual(0, inputService.GetSavedDicesCalled, "Number of calls to GetSavedDices");
            Assert.AreNotEqual(0, inputService.GetUserDecisionCalled, "Number of calls to GetUserDecicion");
        }

        [TestMethod]
        public void PlayTurn_PlayerIsNotInGameAndGetsLessThan1000_ShouldNotBeAskedToStayTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => { return TestUtils.GenerateDices(new[] { 1, 1, 3, 4, 6, 6 }); };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreNotEqual(0, inputService.GetSavedDicesCalled, "Number of calles to GetSavedDices");
            Assert.AreEqual(0, inputService.GetUserDecisionCalled, "Number of calls to GetUsedDecisionCalled");
        }
        
        [TestMethod]
        public void PlayTurn_PlayerIsInTheGameAndGetsSomeScore_ShouldBeAskedToStayTest() {
            var player = new Player();
            player.TotalScore = 1000;
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => {
                return TestUtils.GenerateDices(new[] { 1, 1, 2, 3, 4, 6 });
            };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreNotEqual(0, inputService.GetUserDecisionCalled, "Number of calls to GetUserDecision");
        }

        [TestMethod]
        public void PlayTurn_WhenPlayerEntersGame_ScoreShouldBeAddedToTotalScoreTest() {
            var player = new Player();
            var inputService = new InputServiceMock();
            inputService.RollDicesMock = (p, d) => {
                return TestUtils.GenerateDices(new[] { 1, 1, 1, 3, 4, 6 });
            };
            var gameService = new GameService(inputService);

            gameService.PlayTurn(player);

            Assert.AreEqual(1000, player.TotalScore);
        }

        // antal tärningar vid andra slaget...
    }
}
