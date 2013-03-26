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
            if (this.MediaPlay != null)
            {
                this.IsPlaying = true;

                this.Playlist = new Playlist();
                this.Playlist.Add(new Song(@"..\..\..\Media\Attack.mp3"));
                this.MediaPlay(this, EventArgs.Empty);
            }
        }

        public void Pause()
        {
            if (this.MediaPause != null)
            {
                this.IsPlaying = false;

                this.MediaPause(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (this.MediaStop != null)
            {
                this.IsPlaying = false;

                this.MediaStop(this, EventArgs.Empty);
            }
        }

        public event EventHandler MediaPlay;
        public event EventHandler MediaPause;
        public event EventHandler MediaStop;

        public Playlist Playlist { get; set; }

        public bool IsPlaying { get; set; }
        public bool IsShuffling { get; set; }
        public bool IsRepeatingOne { get; set; }
        public bool IsRepeatingAll { get; set; }
    }
}
