using PinBowlingScoreCalculator.Models;
using System.Threading.Tasks;

namespace PinBowlingScoreCalculator
{
    public interface IGameScore
    {
        void Roll(int pins);

        int GetScore();
    }
}
