using System;
using System.ComponentModel;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    class MediaViewModel : INotifyPropertyChanged
    {
        private readonly Control control_;

        public MediaViewModel()
        {
            this.control_ = Control.Instance;

            this.control_.MediaPlay += (sender, args) =>
                {
                    if (this.Source == null || this.Source.OriginalString != this.control_.Playlist.Current.Src)
                    {
                        this.Source = new Uri(this.control_.Playlist.Current.Src);
                        if (this.OpenRequest != null)
                            this.OpenRequest(this, EventArgs.Empty);
                    }
                    if (this.PlayRequest != null)
                        this.PlayRequest(this, EventArgs.Empty);
                };
            this.control_.MediaPause += (sender, args) =>
                {
                    if (this.PauseRequest != null)
                        this.PauseRequest(this, EventArgs.Empty);
                };
            this.control_.MediaStop += (sender, args) =>
                {
                    if (this.StopRequest != null)
                        this.StopRequest(this, EventArgs.Empty);
                };
        }

        public Uri Source { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OpenRequest;
        public event EventHandler PlayRequest;
        public event EventHandler PauseRequest;
        public event EventHandler StopRequest;
    }
}
