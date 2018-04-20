using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class ZeroScoreTest
    {
        [Fact]
        public void CheckIfScoreCalculatedIsPerfect()
        {
            var game = new Game();
            Roll(game, 0, 20);
            Assert.Equal(0, game.GetScore());
        }

        private void Roll(Game game, int pins, int times)
        {
            for (int i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }
    }
}
