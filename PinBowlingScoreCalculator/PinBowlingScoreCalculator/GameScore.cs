using PinBowlingScoreCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PinBowlingScoreCalculator
{
    public class GameScore : IGameScore
    {
        private readonly List<int> throws;
        private readonly GameFrame gameFrame = new GameFrame();

        public GameScore()
        {
            throws = new List<int>(Constants.MaximumThrowsPossible);
            throws.AddRange(Enumerable.Repeat(Constants.DefaultThrowValue, Constants.MaximumThrowsPossible));
        }

        public void Roll(int pins)
        {
            // Add your logic here. Add classes as needed.
            var currentIndex = throws.IndexOf(Constants.DefaultThrowValue);
            throws[currentIndex] = pins;
        }

        private void SetFrames()
        {
            var currentThrowIndex = 0; var frameCount = 0;

            while (throws[currentThrowIndex] != Constants.DefaultThrowValue)
            {
                frameCount++;

                if (frameCount == Constants.FramesPerGame) { SetLastFrame(currentThrowIndex); break; }

                var currentFrame = new Frame();
                var pinsKnocked = throws[currentThrowIndex];

                if (pinsKnocked == Constants.StrikeScore)
                {
                    currentFrame.CurrentBowlScore[0] = string.Empty;
                    currentFrame.CurrentBowlScore[1] = Constants.StrikeChar;
                    currentFrame.IsStrike = true;
                    gameFrame.Frames.Add(currentFrame); currentThrowIndex++;
                    continue;
                }

                var pinsKnockedInNextThrow = throws[currentThrowIndex + 1];

                currentFrame.CurrentBowlScore[0] = pinsKnocked.ToString();
                currentFrame.CurrentBowlScore[1] = pinsKnockedInNextThrow.ToString();

                if (pinsKnocked + pinsKnockedInNextThrow == Constants.SpareScore)
                { currentFrame.CurrentBowlScore[1] = Constants.SpareChar; currentFrame.IsSpare = true; }

                gameFrame.Frames.Add(currentFrame); currentThrowIndex += 2;
            }
        }

        private void SetLastFrame(int currentThrowIndex)
        {
            var lastFrame = new Frame { IsLastFrame = true };
            var pinsKnocked = throws[currentThrowIndex];
            var pinsKnockedInNextThrow = throws[currentThrowIndex + 1];

            lastFrame.CurrentBowlScore[0] = pinsKnocked.ToString();

            if (pinsKnocked == Constants.StrikeScore)
            {
                lastFrame.CurrentBowlScore[1] = Constants.StrikeChar;
                lastFrame.IsStrike = true;
                var pinsKnockedInBonusThrow = throws[currentThrowIndex + 2] == Constants.StrikeScore
                    ? Constants.StrikeChar : throws[currentThrowIndex + 2].ToString();
                lastFrame.CurrentBowlScore.Add(pinsKnockedInBonusThrow);
            }

            else if (pinsKnocked + pinsKnockedInNextThrow == Constants.SpareScore)
            {
                lastFrame.CurrentBowlScore[1] = Constants.SpareChar;
                lastFrame.IsSpare = true;
                var pinsKnockedInBonusThrow = throws[currentThrowIndex + 2] == Constants.StrikeScore
                    ? Constants.StrikeChar : throws[currentThrowIndex + 2].ToString();
                lastFrame.CurrentBowlScore.Add(pinsKnockedInBonusThrow);
            }

            else lastFrame.CurrentBowlScore[1] = pinsKnockedInNextThrow.ToString();

            gameFrame.Frames.Add(lastFrame);
        }

        public int GetScore()
        {
            try
            {
                SetFrames();

                for (var frameIndex = 0; frameIndex < Constants.FramesPerGame - 1; frameIndex++)
                {
                    var frame = gameFrame.Frames[frameIndex];
                    gameFrame.TotalScore += GetCurrentFrameScore(frame);
                }
                gameFrame.TotalScore += GetLastFrameScore();
                return gameFrame.TotalScore;
            }
            catch (Exception ex)
            {
                throw;
            }
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

            var currentFrameIndex = gameFrame.Frames.IndexOf(currentFrame);

            nextFrame = gameFrame.Frames[currentFrameIndex + 1];

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
            var lastFrame = gameFrame.Frames[lastFrameIndex];
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