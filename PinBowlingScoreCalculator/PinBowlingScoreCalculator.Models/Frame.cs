
namespace PinBowlingScoreCalculator.Models
{
    public class Frame
    {
        public bool IsStrike { get; set; } = false;

        public bool IsSpare { get; set; } = false;

        public bool IsLastFrame { get; set; } = false;

        public bool IsBonus { get; set; } = false;

        public string[] CurrentBowlScore { get; set; } = new string[Constants.ThrowsPerFrame];

        public string BonusScore { get; set; }
    }
}
