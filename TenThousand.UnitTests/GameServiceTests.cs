
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TenThousand.GameEngine.Entities;
using Rhino.Mocks;
using TenThousand.GameEngine.Interfaces;
using TenThousand.GameEngine;
using System.Collections.Generic;

namespace TenThousand.UnitTests {
    [TestClass]
    public class GameServiceTests {
        [TestMethod]
        public void PlayTurnTest() {
            var player = new Player();
            var dices = TestUtils.GenerateDices(new[] { 2, 2, 3, 4, 4, 6 });
            var inputService = MockRepository.GenerateMock<IPlayerInputService>();

            // Expect dices to be rolled once
            inputService.Expect(x => x.RollDices(Arg.Is<Player>(player), Arg<IEnumerable<IDice>>.Is.Anything))
                .Repeat.Once()
                .Return(dices);
            var gameService = new GameService(inputService);

            // Play turn
            gameService.playTurn(player);

            inputService.VerifyAllExpectations();
        }
    }
}
