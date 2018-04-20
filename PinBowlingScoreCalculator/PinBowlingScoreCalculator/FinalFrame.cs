using System.Collections.Generic;

namespace PinBowlingScoreCalculator
{
    public class FinalFrame : IFinalFrame
    {
        public bool IsStrike { get; set; }
        public bool IsSpare { get; set; }
        public List<Throw> Throws { get; set; }

        public FinalFrame BuildFrame(int firstThrow, int secondThrow, int thirdThrow)
        {
            var resultingFrame = new FinalFrame
            {
                Throws = new List<Throw>
                {
                    new Throw(firstThrow),
                    new Throw(secondThrow)
                }
            };

            if (firstThrow == Constants.StrikeScore) resultingFrame.IsStrike = true;
            if (firstThrow + secondThrow == Constants.SpareScore) resultingFrame.IsSpare = true;

            if (resultingFrame.IsStrike || resultingFrame.IsSpare)
                resultingFrame.Throws.Add(new Throw(thirdThrow));

            return resultingFrame;
        }

        public int GetScore(FinalFrame frame)
        {
            var frameScore = 0;

            if(!frame.IsStrike&&!frame.IsSpare)
            {
                for (var throwIndex = 0; throwIndex < Constants.ThrowsPerFrame; throwIndex++)
                    frameScore += frame.Throws[throwIndex].Score;
            }
            else
            {
                if (frame.IsStrike)
                    frameScore = Constants.StrikeBonus * Constants.StrikeScore;
                else
                    frameScore = Constants.SpareBonus * Constants.SpareScore;

                frameScore += frame.Throws[Constants.ThrowsPerFrame].Score;
            }

            return frameScore;
        }
    }
}
