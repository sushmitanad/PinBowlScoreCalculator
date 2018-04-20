using System.Collections.Generic;

namespace PinBowlingScoreCalculator
{
    public class Frame : IFrame
    {
        public bool IsStrike { get; set; }
        public bool IsSpare { get; set; }
        public List<Throw> Throws { get; set; } = new List<Throw>();

        public Frame BuildFrame(int firstThrow, int secondThrow)
        {
            var resultingFrame = new Frame();

            resultingFrame.Throws.Add(new Throw(firstThrow));

            if(firstThrow==Constants.StrikeScore)
            { resultingFrame.IsStrike = true; return resultingFrame; }

            if (firstThrow + secondThrow == Constants.SpareScore) resultingFrame.IsSpare = true;

            resultingFrame.Throws.Add(new Throw(secondThrow));
            return resultingFrame;
        }

        public int GetScore(List<Frame> frames, FinalFrame finalFrame)
        {
            var currentFrame = frames[0];
            var firstNextFrame = frames[1];
            var secondNextFrame = frames[2];

            var frameScore = 0;

            if(currentFrame.IsStrike)
            {
                frameScore = Constants.StrikeScore;
                frameScore += GetStrikeBonus(firstNextFrame, secondNextFrame, finalFrame);
            }
            else if(currentFrame.IsSpare)
            {
                frameScore = Constants.SpareScore;
                frameScore += GetSpareBonus(firstNextFrame, finalFrame);
            }
            else
            {
                frameScore += currentFrame.Throws[0].Score;
                frameScore += currentFrame.Throws[1].Score;
            }

            return frameScore;
        }

        private int GetSpareBonus(Frame nextFrame, FinalFrame finalFrame)
        {
            if(nextFrame!=null)
            {
                if (nextFrame.IsStrike) return Constants.StrikeScore;
                return nextFrame.Throws[0].Score;
            }

            if (finalFrame.IsStrike) return Constants.StrikeScore;
            return finalFrame.Throws[0].Score;
        }

        private int GetStrikeBonus(Frame firstNextFrame, Frame secondNextFrame, FinalFrame finalFrame)
        {
            if (firstNextFrame == null) return finalFrame.Throws[0].Score + finalFrame.Throws[1].Score;

            if (firstNextFrame.IsStrike) return Constants.StrikeScore + GetNextThrowScore(secondNextFrame, finalFrame);

            if (firstNextFrame.IsSpare) return Constants.SpareScore;

            return firstNextFrame.Throws[0].Score + firstNextFrame.Throws[1].Score;
        }

        private int GetNextThrowScore(Frame nextFrame, FinalFrame finalFrame)
        {
            if(nextFrame!=null)
            {
                if (nextFrame.IsStrike) return Constants.StrikeScore;
                if (nextFrame.IsSpare) return Constants.SpareBonus;
                return nextFrame.Throws[0].Score;
            }
            if (finalFrame.IsStrike) return Constants.StrikeScore;
            if (finalFrame.IsSpare) return Constants.SpareBonus;
            return finalFrame.Throws[0].Score;
        }
    }
}
