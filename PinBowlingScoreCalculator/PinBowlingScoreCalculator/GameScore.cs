using PinBowlingScoreCalculator.Models;
using System;
using System.Threading.Tasks;

namespace PinBowlingScoreCalculator
{
    public class GameScore : IGameScore
    {
        private readonly GameFrame gameFrame;

        public GameScore(GameFrame gameFrame)
        {
            this.gameFrame = gameFrame;
        }

        public async Task<int> CalculateScore()
        {
            try
            {
                for (int i = 0; i < Constants.FramesPerGame - 1; i++)
                {
                    for (int j = 0; j < Constants.ThrowsPerFrame; j++)
                    {
                        gameFrame.TotalScore += GetCurrentThrowScore(i, j);
                        if (gameFrame.frames[i].IsStrike)
                            for (int next = 0; next < Constants.StrikeBonus; next++)
                                gameFrame.TotalScore += GetNextThrowScore(i, j, next);
                        if (gameFrame.frames[i].IsSpare)
                            for (int next = 0; next < Constants.SpareBonus; next++)
                                gameFrame.TotalScore += GetNextThrowScore(i, j, next);
                    }
                }

                return gameFrame.TotalScore;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private int GetCurrentThrowScore(int currentFrame, int currentThrow)
        {
            if (int.TryParse(gameFrame.frames[currentFrame].CurrentBowlScore[currentThrow], out int score))
                return score;

            if (string.Equals(gameFrame.frames[currentFrame].CurrentBowlScore[currentThrow].ToUpper(), Constants.StrikeChar))
            {
                SetStrike(currentFrame);
                return Constants.StrikeScore;
            }

            if (string.Equals(gameFrame.frames[currentFrame].CurrentBowlScore[currentThrow], Constants.SpareChar))
            {
                SetSpare(currentFrame);
                return Constants.SpareScore;
            }

            return 0;
        }
        
        private void SetStrike(int curretFrame)
        {
            gameFrame.frames[curretFrame].IsStrike = true;
        }

        private void SetSpare(int curretFrame)
        {
            gameFrame.frames[curretFrame].IsSpare = true;
        }

        private int GetNextThrowScore(int currentFrame, int currentThrow, int nextRequiredIndex)
        {
            var currentNextIndex = -1;
            while (true)
            {
                if (currentThrow == Constants.ThrowsPerFrame - 1)
                {
                    currentFrame++;
                    currentThrow = 0;
                    if (currentFrame == Constants.FramesPerGame - 1)
                        return 0;
                    if (string.IsNullOrEmpty(gameFrame.frames[currentFrame].CurrentBowlScore[currentThrow].ToString()))
                        currentNextIndex++;
                    if (currentNextIndex == nextRequiredIndex)
                        break;
                }
                if (currentThrow < Constants.ThrowsPerFrame)
                {
                    currentNextIndex++;
                    currentThrow++;
                    if (currentNextIndex == nextRequiredIndex)
                        break;
                }
            }
            return GetCurrentThrowScore(currentFrame, currentThrow);
        }
    }
}
