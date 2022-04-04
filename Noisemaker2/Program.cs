using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Noisemaker2
{
    class Program
    {
        public static int windowWidth = 700;
        public static int widnowHeight = 900;

        public static int fontSize = 20;

        static void Main(string[] args)
        {
            Raylib.InitWindow(windowWidth, widnowHeight, "Music player");
            Raylib.InitAudioDevice();
            SoundPlayer player = new SoundPlayer();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                player.Update();
                player.Draw();
                Raylib.EndDrawing();
            }
        }
    }
}
