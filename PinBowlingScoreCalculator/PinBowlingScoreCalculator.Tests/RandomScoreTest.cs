using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class RandomScoreTest
    {
        [Fact]
        public void CheckIfScoreCalculatedIsPerfect()
        {
            var game = new GameScore();
            Roll(game, 0, 20);
            Assert.Equal(187, game.GetScore());
        }

        private void Roll(GameScore game, int pins, int times)
        {
            game.Roll(10);
            game.Roll(9);
            game.Roll(1);
            game.Roll(5);
            game.Roll(5);
            game.Roll(7);
            game.Roll(2);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(9);
            game.Roll(0);
            game.Roll(8);
            game.Roll(2);
            game.Roll(9);
            game.Roll(1);
            game.Roll(10);
        }
    }
}
