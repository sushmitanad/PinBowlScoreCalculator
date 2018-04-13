using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace PinBowlingScoreCalculator.Tests
{
    public class ExceptionTest
    {
        private readonly IGameScore gameScore;

        public ExceptionTest()
        {
            var frames = new List<Frame>();
            for (int i = 0; i < 3; i++)
                frames.Add(new Frame { CurrentBowlScore = new List<string>(new[] { "-", "-" }) });
            var gameFrame = new GameFrame
            {
                FrameId = Guid.NewGuid(),
                frames = frames
            };

            gameScore = new GameScore(gameFrame);
        }

        [Fact]
        public async void ShouldThrowException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => gameScore.CalculateScore());
        }
    }
}
