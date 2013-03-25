using System;

namespace MyWmp.Models
{
    class Control
    {
        private static Control instance_;

        public static Control Instance
        {
            get { return instance_ ?? (instance_ = new Control()); }
        }

        private Control()
        {
            this.IsPlaying = false;
            this.IsShuffling = false;
            this.IsRepeatingOne = false;
            this.IsRepeatingAll = false;
        }

        public void Play()
        {
            this.IsPlaying = true;

            this.Playlist = new Playlist();
            this.Playlist.Add(new Song(@"C:\Users\Fiahil\Music\StarCraft II - Heart of the Swarm\02 Heart of the Swarm.mp3"));

            if (this.MediaPlay != null)
                this.MediaPlay(this, EventArgs.Empty);
        }

        public void Pause()
        {
            this.IsPlaying = false;
        }

        public void Stop()
        {
            this.IsPlaying = false;
        }

        public event EventHandler MediaPlay;

        public Playlist Playlist { get; set; }

        public bool IsPlaying { get; set; }
        public bool IsShuffling { get; set; }
        public bool IsRepeatingOne { get; set; }
        public bool IsRepeatingAll { get; set; }
    }
}
