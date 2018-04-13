using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class RandomScoreTest
    {
        private readonly IGameScore gameScore;

        public RandomScoreTest()
        {
            var frames = new List<Frame>
            {
                new Frame { CurrentBowlScore = new List<string>(new[] { "", "x" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "9", "/" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "5", "/" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "7", "2" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "", "x" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "", "x" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "", "x" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "9", "-" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "8", "/" }) },
                new Frame { CurrentBowlScore = new List<string>(new[] { "9", "/", "x" }) }
            };

            var gameFrame = new GameFrame
            {
                FrameId = Guid.NewGuid(),
                frames = frames
            };

            gameScore = new GameScore(gameFrame);
        }

        [Fact]
        public async void ShouldGenerateExpectedScore()
        {
            var expectedScore = 187;

            var actualScore = await gameScore.CalculateScore();

            Assert.Equal(expectedScore, actualScore);
        }
    }
}
