
using System.Collections.Generic;
using System.Linq;

namespace PinBowlingScoreCalculator.Models
{
    public class Frame
    {
        public bool IsStrike { get; set; } = false;

        public bool IsSpare { get; set; } = false;

        public bool IsLastFrame { get; set; } = false;

        public bool IsBonus { get; set; } = false;

        public List<string> CurrentBowlScore { get; set; } = new List<string>();

        public Frame()
        {
            CurrentBowlScore.AddRange(Enumerable.Repeat(Constants.DefaultThrowValue.ToString(), Constants.ThrowsPerFrame));
        }
    }
}
