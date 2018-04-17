using System;
using Xunit;
using PinBowlingScoreCalculator.Models;
using System.Collections.Generic;

namespace PinBowlingScoreCalculator.Tests
{
    public class PerfectScoreTest
    {
        [Fact]
        public void CheckIfScoreCalculatedIsPerfect()
        {
            var game = new GameScore();
            Roll(game, 10, 12);
            Assert.Equal(300, game.GetScore());
        }

        private void Roll(GameScore game, int pins, int times)
        {
            for (int i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }
    }
}
