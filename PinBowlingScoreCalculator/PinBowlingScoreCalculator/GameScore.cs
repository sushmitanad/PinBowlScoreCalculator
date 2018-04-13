using PinBowlingScoreCalculator.Models;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace PinBowlingScoreCalculator
{
    public class GameScore : IGameScore
    {
        private readonly GameFrame gameFrame;

        public GameScore(GameFrame gameFrame)
        {
            this.gameFrame = gameFrame;
            SetFrameTypes();
        }

        private void SetFrameTypes()
        {
            foreach (var frame in gameFrame.frames)
            {
                var endChar = frame.CurrentBowlScore[Constants.ThrowsPerFrame - 1];
                if (string.Equals(endChar, Constants.StrikeChar, StringComparison.CurrentCultureIgnoreCase))
                    frame.IsStrike = true;
                if (string.Equals(endChar, Constants.SpareChar, StringComparison.CurrentCultureIgnoreCase))
                    frame.IsSpare = true;
            }
            gameFrame.frames[Constants.FramesPerGame - 1].IsLastFrame = true;
        }

        public async Task<int> CalculateScore()
        {
            for (var frameIndex = 0; frameIndex < Constants.FramesPerGame - 1; frameIndex++)
            {
                var frame = gameFrame.frames[frameIndex];
                gameFrame.TotalScore += GetCurrentFrameScore(frame);
            }
            gameFrame.TotalScore += GetLastFrameScore();
            return gameFrame.TotalScore;
        }

        private int GetCurrentFrameScore(Frame currentFrame)
        {
            var frameScore = 0;

            if (currentFrame.IsStrike)
            {
                frameScore = Constants.StrikeScore;
                frameScore += GetStrikeBonus(currentFrame);
            }
            else if (currentFrame.IsSpare)
            {
                frameScore = Constants.SpareScore;
                frameScore += GetSpareBonus(currentFrame);
            }
            else
            {
                foreach (var ballThrow in currentFrame.CurrentBowlScore)
                    if (!string.Equals(ballThrow, Constants.NoScoreChar, StringComparison.CurrentCultureIgnoreCase))
                        frameScore += int.Parse(ballThrow);
            }

            return frameScore;
        }

        private int GetNextThrowScore(Frame currentFrame, out Frame nextFrame)
        {

            if (currentFrame.IsLastFrame) { nextFrame = null; return currentFrame.IsStrike ? Constants.StrikeScore : 0; }

            var currentFrameIndex = gameFrame.frames.IndexOf(currentFrame);

            nextFrame = gameFrame.frames[currentFrameIndex + 1];

            if (nextFrame.IsStrike) return Constants.StrikeScore;

            if (nextFrame.IsSpare && currentFrame.IsStrike) return Constants.StrikeScore;

            var nextThrowChar = nextFrame.CurrentBowlScore[0];
            if (string.Equals(nextThrowChar, Constants.NoScoreChar, StringComparison.CurrentCultureIgnoreCase))
                return 0;

            return int.Parse(nextThrowChar);
        }

        private int GetStrikeBonus(Frame currentFrame)
        {
            var strikeBonus = 0;
            strikeBonus += GetNextThrowScore(currentFrame, out var nextFrame);
            if (nextFrame.IsStrike)
                strikeBonus += GetNextThrowScore(nextFrame, out var oneNextFrame);
            return strikeBonus;
        }

        private int GetSpareBonus(Frame currentFrame)
        {
            return GetNextThrowScore(currentFrame, out var nextFrame);
        }

        private int GetLastFrameScore()
        {
            var lastFrameIndex = Constants.FramesPerGame - 1;
            var lastFrame = gameFrame.frames[lastFrameIndex];
            var frameScore = 0;

            if (!lastFrame.IsStrike && !lastFrame.IsSpare)
            {
                for (var throwIndex = 0; throwIndex < Constants.ThrowsPerFrame; throwIndex++)
                {
                    var throwChar = lastFrame.CurrentBowlScore[throwIndex];
                    if (!string.Equals(throwChar, Constants.NoScoreChar, StringComparison.CurrentCultureIgnoreCase))
                        frameScore += int.Parse(throwChar);
                }
            }
            else
            {
                if (lastFrame.IsStrike)
                    frameScore += Constants.StrikeBonus * Constants.StrikeScore;
                else
                    frameScore += Constants.SpareBonus * Constants.SpareScore;

                var bonusScoreChar = lastFrame.CurrentBowlScore[Constants.ThrowsPerFrame - 1];
                if (string.Equals(bonusScoreChar, Constants.StrikeChar, StringComparison.CurrentCultureIgnoreCase))
                    frameScore += Constants.StrikeScore;
                else if (string.Equals(bonusScoreChar, Constants.SpareChar, StringComparison.CurrentCultureIgnoreCase))
                    frameScore += Constants.SpareScore;
                else if (!string.Equals(bonusScoreChar, Constants.NoScoreChar, StringComparison.CurrentCultureIgnoreCase))
                    frameScore += int.Parse(bonusScoreChar);
            }

            return frameScore;
        }
    }
}