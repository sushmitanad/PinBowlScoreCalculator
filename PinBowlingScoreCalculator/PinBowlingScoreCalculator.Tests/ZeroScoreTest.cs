using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class ZeroScoreTest
    {
        [Fact]
        public void CheckIfScoreCalculatedIsPerfect()
        {
            var game = new GameScore();
            Roll(game, 0, 20);
            Assert.Equal(0, game.GetScore());
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
