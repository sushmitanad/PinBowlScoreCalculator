
using System.Collections.Generic;

namespace PinBowlingScoreCalculator.Models
{
    public class Frame
    {
        public bool IsStrike { get; set; } = false;

        public bool IsSpare { get; set; } = false;

        public bool IsLastFrame { get; set; } = false;

        public bool IsBonus { get; set; } = false;

        public List<string> CurrentBowlScore { get; set; } = new List<string>(Constants.ThrowsPerFrame);
    }
}
