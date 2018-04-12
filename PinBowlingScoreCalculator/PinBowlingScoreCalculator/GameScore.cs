using PinBowlingScoreCalculator.Models;
using System;
using System.Threading.Tasks;

namespace PinBowlingScoreCalculator
{
    public class GameScore : IGameScore
    {
        public async Task<int> CalculateScore(GameFrame gameFrame)
        {
            throw new NotImplementedException();
        }
    }
}
