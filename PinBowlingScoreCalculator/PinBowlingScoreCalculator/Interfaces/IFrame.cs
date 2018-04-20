using System.Collections.Generic;

namespace PinBowlingScoreCalculator
{
    public interface IFrame
    {
        int GetScore(List<Frame> frames, FinalFrame finalFrame);

        Frame BuildFrame(int firstThrow, int secondThrow);
    }
}
