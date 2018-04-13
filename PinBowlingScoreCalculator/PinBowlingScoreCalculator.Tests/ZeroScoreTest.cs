using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class ZeroScoreTest
    {
        private readonly IGameScore gameScore;

        public ZeroScoreTest()
        {
            var frames = new List<Frame>();
            for (int i = 0; i < 10; i++)
                frames.Add(new Frame { CurrentBowlScore = new List<string>(new[] { "-", "-" }) });
            var gameFrame = new GameFrame
            {
                FrameId = Guid.NewGuid(),
                frames = frames
            };

            gameScore = new GameScore(gameFrame);
        }

        [Fact]
        public async void CheckIfScoreCalculatedIsZero()
        {
            var expectedScore = 0;

            var actualScore = await gameScore.CalculateScore();

            Assert.Equal(expectedScore, actualScore);
        }
    }
}
