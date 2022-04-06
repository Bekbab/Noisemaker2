using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Noisemaker2
{
    public class SoundPlayer
    {
        public static Queue<string> musicQueue;
        Dictionary<string, Music> songs;

        string currentSongString;
        Music currentSong;

        bool playing;

        ProgressBar progbar = new ProgressBar();

        List<Songbutton> buttons;

        public static bool torskMode = false;

        Texture2D torskImage;

        bool testMode;

        Songbutton testButton;
        public SoundPlayer()
        {
            List<string> songNames = new List<string>()  //adds all the song titles to the list "soundNames"
            {
                "Blues",
                "Friends",
                "Infested_City",
                "Intergalactic_Odyssey",
                "Interplanetary_Odyssey",
                "No_Place_For_Straw_Cowboys",
                "Shinrin_Yoku",
                "torsk"
            };
            songs = new Dictionary<string, Music>();

            foreach (string name in songNames) //loads all the files into the dictionary "songs" with their song title as their key
            {
                songs.Add(name, Raylib.LoadMusicStream(name + ".ogg"));
            }

            songs.Add("test", Raylib.LoadMusicStream("test.ogg"));

            musicQueue = new Queue<string>();

            //ProgressBar progbar = new ProgressBar();

            buttons = new List<Songbutton>();

            for (int i = 0; i < songNames.Count; i++)
            {
                buttons.Add(new Songbutton(songNames[i], (50 * i) + 30));
            }

            torskImage = Raylib.LoadTexture("torsk.png");
        }

        public bool IsQueueEmpty()
        {
            return !musicQueue.TryPeek(out string result); //If the queue is epmty trypeek returns "false", the return value is thererefore reversed so that when the queue is empty IsQueueEmpty returns true.
        }
        public Music GetSong()
        {
            if (!IsQueueEmpty())
            {
                currentSongString = musicQueue.Peek();  //peeks at what song title is currently in queue
                currentSong = songs[currentSongString]; //gets which song that title refers to
            }

            return currentSong;
        }
        public void PlayPause() //pauses or plays the music
        {
            playing = !playing;

            if (!playing)
            {
                Raylib.PauseMusicStream(GetSong()); //pause
            }
            else
            {
                Raylib.ResumeMusicStream(GetSong()); //resume
            }
        }
        public void Restart() //restarts the current song
        {
            Raylib.StopMusicStream(GetSong());
            Raylib.PlayMusicStream(GetSong());
        }
        public void Skip() //skip to the next song in the queue
        {
            Raylib.StopMusicStream(GetSong());
            musicQueue.Dequeue();
        }

        public void Player()
        {
            if (torskMode)
            {
                Raylib.PlayMusicStream(GetSong());
            }
            else
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) //press space to pause and play
                {
                    PlayPause();
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))//press enter to skip a song
                {
                    Skip();
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))//press R to restart a song
                {
                    Restart();
                }
                if (playing) //plays the music if it's supposed to be playing. 
                {
                    Raylib.PlayMusicStream(GetSong());
                }

            }

        }
        public void Update()
        {

            if (!IsQueueEmpty())
            {

                if (Raylib.GetMusicTimePlayed(GetSong()) >= Raylib.GetMusicTimeLength(GetSong()) - 0.1) //if the song has reached its end... (needs to remove one frame from the song to give it an attempt to skip)
                {

                    Skip(); //...it skips to the next song (This doesnt work)
                }

                if (currentSongString == "torsk")
                {
                    torskMode = true;
                }
                else
                {
                    foreach (Songbutton button in buttons)
                    {
                        button.Update();
                    }
                }

                Raylib.UpdateMusicStream(GetSong());
                Player();
                progbar.Update(Raylib.GetMusicTimePlayed(GetSong()), Raylib.GetMusicTimeLength(GetSong())); //updates the progress bar, giving it the current song's length and how long it has been playing



            }
            else
            {
                progbar.Update(0, 0);

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_T))
                {
                    testMode = !testMode;

                    if (!testMode)
                    {
                        buttons.Add(testButton = new Songbutton("test", 850));
                        testButton.button.x = 20;
                    }
                    else
                    {
                        buttons.Remove(testButton);
                    }
                }

                foreach (Songbutton button in buttons)
                {
                    button.Update();
                }
            }
        }


        public void Draw()
        {
            if (torskMode)
            {
                Raylib.ClearBackground(Color.GREEN);
                Raylib.DrawTexture(torskImage, 100, 100, Color.WHITE);
                int torskWidth = Raylib.MeasureText("torsk", Program.fontSize);
                Raylib.DrawText("torsk", (Program.windowWidth / 2) - (torskWidth / 2), 650, Program.fontSize, Color.WHITE);
            }
            else
            {
                Raylib.ClearBackground(Color.BLACK);
                foreach (Songbutton button in buttons)
                {
                    button.Draw();
                }
                Raylib.DrawText("Space: pause/play", 10, 400, Program.fontSize, Color.WHITE);
                Raylib.DrawText("Enter: Skip song", 10, 430, Program.fontSize, Color.WHITE);
                Raylib.DrawText("R: Restart song", 10, 460, Program.fontSize, Color.WHITE);
                Raylib.DrawText("Click the song you want to hear to add it to the queue", 10, 490, Program.fontSize, Color.WHITE);
                if (!IsQueueEmpty())
                {
                    int playingWidth = Raylib.MeasureText($"Next song: {currentSongString}", Program.fontSize);
                    Raylib.DrawText($"Now playing: {currentSongString}", (Program.windowWidth / 2) - (playingWidth / 2), 650, Program.fontSize, Color.WHITE);
                }
                if (IsQueueEmpty())
                {
                    int addWidth = Raylib.MeasureText("Add a song and press play...", Program.fontSize);
                    Raylib.DrawText("Add a song and press play...", (Program.windowWidth / 2) - (addWidth / 2), 650, Program.fontSize, Color.WHITE);
                }
            }
            progbar.Draw();

        }


    }
}