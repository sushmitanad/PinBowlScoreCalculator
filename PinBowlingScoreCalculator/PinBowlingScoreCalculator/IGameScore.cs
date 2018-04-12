using PinBowlingScoreCalculator.Models;
using System.Threading.Tasks;

namespace PinBowlingScoreCalculator
{
    public interface IGameScore
    {
        Task<int> CalculateScore(GameFrame gameFrame);
    }
}
