using System.Collections.Generic;

namespace PinBowlingScoreCalculator
{
    public class Game
    {
        private readonly IFrame frame;
        private readonly IFinalFrame finalFrame;
        private List<int> PinsRolled { get; set; } = new List<int>();

        public int GameScore { get; set; }
        public List<Frame> Frames { get; set; } = new List<Frame>();
        public FinalFrame FinalFrame { get; set; }

        public Game()
        {
            frame = new Frame();
            finalFrame = new FinalFrame();
        }

        public int GetScore()
        {
            BuildGame();

            var frameIndex = 0;
            while(frameIndex<Constants.FramesPerGame-1)
            {
                var frames = new List<Frame>();
                if(frameIndex==Constants.FramesPerGame-2)//For 9th frame
                {
                    frames.Add(Frames[frameIndex]); frames.Add(null); frames.Add(null);
                }
                else if(frameIndex==Constants.FramesPerGame-3)//For 8th frame
                {
                    frames.Add(Frames[frameIndex]); frames.Add(Frames[frameIndex + 1]); frames.Add(null);
                }
                else
                {
                    frames.Add(Frames[frameIndex]); frames.Add(Frames[frameIndex + 1]); frames.Add(Frames[frameIndex + 2]);
                }

                GameScore += frame.GetScore(frames, FinalFrame);
                frameIndex++;
            }

            GameScore += finalFrame.GetScore(FinalFrame);

            return GameScore;
        }

        public void Roll(int pins)
        {
            PinsRolled.Add(pins);
        }

        private void BuildGame()
        {
            var pinCount = 0;
            var frameCount = 0;
            int firstThrow, secondThrow, thirdThrow = 0;

            while(frameCount<Constants.FramesPerGame-1)
            {
                firstThrow = PinsRolled[pinCount];
                secondThrow = PinsRolled[pinCount + 1];

                var currentFrame = frame.BuildFrame(firstThrow, secondThrow);
                Frames.Add(currentFrame);
                frameCount++;

                if (currentFrame.IsStrike) pinCount++;
                else pinCount += 2;
            }

            firstThrow = PinsRolled[pinCount];
            secondThrow = PinsRolled[pinCount + 1];
            if (pinCount + 2 < PinsRolled.Count) thirdThrow = PinsRolled[pinCount + 2];
            var gameFinalFrame = finalFrame.BuildFrame(firstThrow, secondThrow, thirdThrow);
            FinalFrame = gameFinalFrame;
        }
    }
}
