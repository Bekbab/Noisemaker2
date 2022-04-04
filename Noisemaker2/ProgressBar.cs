using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Noisemaker2
{
    public class ProgressBar
    {
        public Rectangle background;
        Rectangle progress;

        public float playerProgress;
        public ProgressBar()
        {
            background = new Rectangle((Program.windowWidth / 2) - 300, 700, 600, 10); //the progress background is defined, it is centered in the screen
            progress = new Rectangle(background.x, background.y, 0, background.height);

        }
        public void Update(float played, float length)
        {
            playerProgress = played / length * background.width;
            progress.width = playerProgress;
        }
        public void Draw()
        {
            Raylib.DrawRectangleRec(background, Color.GRAY);
            Raylib.DrawRectangleRec(progress, Color.WHITE);
        }
    }
}