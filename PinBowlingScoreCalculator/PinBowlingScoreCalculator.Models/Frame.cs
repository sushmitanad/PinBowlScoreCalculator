
namespace PinBowlingScoreCalculator.Models
{
    public class Frame
    {
        public bool IsStrike { get; set; }

        public bool IsSpare { get; set; }

        public bool IsEndOfFrame { get; set; }

        public char CurrentBowlScore { get; set; }
    }
}
