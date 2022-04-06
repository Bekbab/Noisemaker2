using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Noisemaker2
{
    public class Songbutton
    {
        public Rectangle button;
        int width;

        bool isHovering;

        string name;

        public Songbutton(string songName, float recY)
        {
            if (songName == "torsk")
            {
                recY = 850;
            }
            width = Raylib.MeasureText(songName, Program.fontSize);
            button = new Rectangle((Program.windowWidth / 2) - (width / 2), recY, width + 10, Program.fontSize + 10);
            name = songName;

        }

        bool getHover()
        {
            if (Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), button))
            {
                isHovering = true;
            }
            else
            {
                isHovering = false;
            }

            return isHovering;
        }
        public void Update()
        {
            if (getHover() && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                SoundPlayer.musicQueue.Enqueue(name);
            }
        }
        public void Draw()
        {
            if (getHover())
            {
                if (name == "torsk")
                {
                    Raylib.DrawRectangleRec(button, Color.BROWN);
                    Raylib.DrawText("are you sure about this?", (int)button.x + 5, (int)button.y + 5, Program.fontSize, Color.GREEN);
                }
                else
                {
                    Raylib.DrawRectangleRec(button, Color.RED);
                }
            }
            else
            {
                Raylib.DrawRectangleRec(button, Color.GRAY);
            }

            if (name == "torsk" && !getHover())
            {
                Raylib.DrawText(name, (int)button.x + 5, (int)button.y + 5, Program.fontSize, Color.BLACK);
            }
            else if (name != "torsk")
            {
                Raylib.DrawText(name, (int)button.x + 5, (int)button.y + 5, Program.fontSize, Color.BLACK);
            }

        }
    }
}