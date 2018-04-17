using System;
using System.Collections.Generic;

namespace PinBowlingScoreCalculator.Models
{
    public class GameFrame
    {
        public Guid FrameId { get; set; } = Guid.NewGuid();

        public int TotalScore { get; set; } = 0;

        public List<Frame> Frames { get; set; } = new List<Frame>();
    }
}
