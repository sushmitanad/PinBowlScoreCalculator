using System;
using System.Collections.Generic;

namespace PinBowlingScoreCalculator.Models
{
    public class GameFrame
    {
        public Guid FrameId { get; set; }

        public int TotalScore { get; set; }

        public List<Frame> frames = new List<Frame>(Constants.FramesPerGame);
    }
}
