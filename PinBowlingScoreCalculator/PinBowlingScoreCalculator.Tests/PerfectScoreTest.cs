using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class PerfectScoreTest
    {
        [Fact]
        public void CheckIfScoreCalculatedIsPerfect()
        {
            var game = new Game();
            Roll(game, 10, 12);
            Assert.Equal(300, game.GetScore());
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
