using System;
using Xunit;
using PinBowlingScoreCalculator.Models;
using System.Collections.Generic;

namespace PinBowlingScoreCalculator.Tests
{
    public class PerfectScoreTest
    {
        private readonly IGameScore gameScore;

        public PerfectScoreTest()
        {
            gameScore = new GameScore();
        }

        [Fact]
        public async void CheckIfScoreCalculatedIsPerfect()
        {
            var expectedScore = 300;
            var frames = new List<Frame>();
            for (int i = 0; i < 12; i++)
                frames.Add(new Frame { CurrentBowlScore = 'x' });
            var gameFrame = new GameFrame
            {
                FrameId = Guid.NewGuid(),
                frames = frames
            };

            var actualScore = await gameScore.CalculateScore(gameFrame);

            Assert.Equal(expectedScore, actualScore);
        }
    }
}
