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
            this.IsRepeatingAll = false;
            this.Speed = 1;
        }

        public void Play()
        {
            if (this.MediaPlay != null && Playlist != null)
            {
                if (Playlist.Current == null)
                    Playlist.Reset();
                if (Playlist.Current != null)
                {
                    this.MediaStop(this, EventArgs.Empty);
                    this.IsPlaying = true;
                    this.MediaPlay(this, EventArgs.Empty);
                }
            }
        }

        public void Pause()
        {
            if (this.MediaPause != null && Playlist != null)
            {
                this.IsPlaying = false;

                this.MediaPause(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (this.MediaStop != null && Playlist != null)
            {
                this.IsPlaying = false;

                this.MediaStop(this, EventArgs.Empty);
            }
        }

        public void Next()
        {
            if (this.MediaPlay != null && Playlist != null)
            {
                this.Playlist.Next();
                if (IsPlaying)
                {
                    if (Playlist.Current != null)
                        this.MediaPlay(this, EventArgs.Empty);
                    else
                        Stop();
                }
            }
        }

        public void Prev()
        {
            if (this.MediaPlay != null && Playlist != null)
            {
                this.Playlist.Prev();
                if (IsPlaying)
                {
                    if (Playlist.Current != null)
                        this.MediaPlay(this, EventArgs.Empty);
                    else
                        Stop();
                }
            }
        }

        public void Shuffle()
        {
            IsShuffling = IsShuffling == false;
            if (Playlist != null)
                Playlist.Shuffle = IsShuffling;
        }

        public void RepeatAll()
        {
            IsRepeatingAll = IsRepeatingAll == false;
            if (Playlist != null)
               Playlist.RepeatAll = IsRepeatingAll;
        }

        public void SpeedUp()
        {
            if (this.Speed < 4)
                this.Speed += 0.25;
            if (this.MediaSpeed != null)
                this.MediaSpeed(this, EventArgs.Empty);
        }

        public void SpeedDown()
        {
            if (this.Speed > 0.25)
                this.Speed -= 0.25;
            if (this.MediaSpeed != null)
                this.MediaSpeed(this, EventArgs.Empty);
        }

        public void SpeedReset()
        {
            this.Speed = 1;
            if (this.MediaSpeed != null)
                this.MediaSpeed(this, EventArgs.Empty);
        }

        public event EventHandler MediaPlay;
        public event EventHandler MediaPause;
        public event EventHandler MediaStop;
        public event EventHandler MediaSpeed;

        public event EventHandler PlaybackListRefresh;

        private Playlist playlist_;
        public Playlist Playlist {
            get { return playlist_; }
            set { playlist_ = value;
                if (this.PlaybackListRefresh != null) this.PlaybackListRefresh(this, EventArgs.Empty);
            }
        }

        public double Speed { get; private set; }

        public bool IsPlaying { get; private set; }
        public bool IsShuffling { get; private set; }
        public bool IsRepeatingAll { get; private set; }
    }
}
