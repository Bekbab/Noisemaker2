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
            progress = new Rectangle(background.x, background.y, 0, background.height); //progress rec is based on background with undefined width

        }
        public void Update(float played, float length) //update method needs the song length and how long it has been playing for
        {
            playerProgress = played / length * background.width;
            progress.width = playerProgress;
        }
        public void Draw()
        {
            //draw the progress bar and background
            Raylib.DrawRectangleRec(background, Color.GRAY);
            Raylib.DrawRectangleRec(progress, Color.WHITE);
        }
    }
}