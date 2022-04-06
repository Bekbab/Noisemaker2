using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Noisemaker2
{
    class Program
    {
        public static int windowWidth = 700; //window width
        public static int widnowHeight = 900; //window height

        public static int fontSize = 20; //all text will be the same size

        static void Main(string[] args)
        {
            Raylib.InitWindow(windowWidth, widnowHeight, "Music player"); //create window
            Raylib.SetTargetFPS(60); //FPS is set at 60
            Raylib.InitAudioDevice(); //create the audio device
            SoundPlayer player = new SoundPlayer(); //create a sound player

            while (!Raylib.WindowShouldClose()) //main loop
            {
                //updates and draws until the program is closed
                Raylib.BeginDrawing();
                player.Update();
                player.Draw();
                Raylib.EndDrawing();
            }
        }
    }
}
