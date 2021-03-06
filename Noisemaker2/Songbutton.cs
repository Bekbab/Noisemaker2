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
            if (songName == "torsk") //get out of here!
            {
                recY = 880;
            }

            //Measure text width and defines the button so it is drawn in the center of the screen
            width = Raylib.MeasureText(songName, Program.fontSize);
            button = new Rectangle((Program.windowWidth / 2) - (width / 2), recY, width + 10, Program.fontSize + 10);

            if (songName == "torsk") //get out of here!
            {
                button.x = -5;
            }

            name = songName;

        }

        bool GetHover() //checks if you are hovering over the button
        {
            if (Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), button))
            {
                isHovering = true; //you are hovering
            }
            else
            {
                isHovering = false; //you are not hovering
            }

            return isHovering;
        }
        public void Update()
        {
            if (GetHover() && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) //click the button to enqueue the song
            {
                SoundPlayer.musicQueue.Enqueue(name);
            }
        }
        public void Draw()
        {
            if (GetHover())
            {
                if (name == "torsk") //Mind your own business
                {
                    Raylib.DrawRectangleRec(button, Color.BROWN);
                    Raylib.DrawText("are you sure about this?", (int)button.x + 5, (int)button.y + 5, Program.fontSize, Color.GREEN);
                }
                else
                {
                    Raylib.DrawRectangleRec(button, Color.RED); //if you're hovering over the button it turns red (shows that it's clickable)...
                }
            }
            else
            {
                Raylib.DrawRectangleRec(button, Color.GRAY); //...it's otherwise gray
            }

            if (name == "torsk" && !GetHover()) //like I said, this is nothing of importance
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