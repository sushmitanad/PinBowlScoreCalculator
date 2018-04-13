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
            var frames = new List<Frame>();
            for (int i = 0; i < 10; i++)
                frames.Add(new Frame { CurrentBowlScore = new string[] { string.Empty, "x" } });
            frames[9].CurrentBowlScore[0] = "x";
            frames[9].BonusScore = "x";
            var gameFrame = new GameFrame
            {
                FrameId = Guid.NewGuid(),
                frames = frames
            };

            gameScore = new GameScore(gameFrame);
        }

        [Fact]
        public async void CheckIfScoreCalculatedIsPerfect()
        {
            var expectedScore = 300;
            
            var actualScore = await gameScore.CalculateScore();

            Assert.Equal(expectedScore, actualScore);
        }
    }
}
