using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenThousand.GameEngine.Interfaces;

namespace TenThousand.UnitTests {
    public static class TestUtils {

        public static IEnumerable<IDice> GenerateDices(int[] values) {
            foreach (var value in values) {
                var dice = MockRepository.GenerateMock<IDice>();
                dice.Stub(d => d.Value).Return(value);
                yield return dice;
            }
        }

    }
}
