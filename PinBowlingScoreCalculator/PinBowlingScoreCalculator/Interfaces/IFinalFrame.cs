
namespace PinBowlingScoreCalculator
{
    public interface IFinalFrame
    {
        int GetScore(FinalFrame frame);

        FinalFrame BuildFrame(int firstThrow, int secondThrow, int thirdThrow);
    }
}
