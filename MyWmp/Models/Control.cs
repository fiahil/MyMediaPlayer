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

            this.Playlist = new Playlist();

            var loader = new Loader {Root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), FileExtension = new[]{".mp3", ".mp4", ".avi", ".jpg", ".png"}};
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                this.Playlist.Add(new Picture(media));
            }
        }

        public void Play()
        {
            if (this.MediaPlay != null && Playlist != null)
            {
                this.IsPlaying = true;
                if (Playlist.Current == null)
                    Playlist.Reset();
                if (Playlist.Current != null)
                   this.MediaPlay(this, EventArgs.Empty);
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

        public event EventHandler MediaPlay;
        public event EventHandler MediaPause;
        public event EventHandler MediaStop;

        public Playlist Playlist { get; set; }

        public bool IsPlaying { get; private set; }
        public bool IsShuffling { get; private set; }
        public bool IsRepeatingAll { get; private set; }

    }
}
