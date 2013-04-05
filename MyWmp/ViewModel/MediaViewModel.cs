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
                    if (this.Source == null || this.Source.OriginalString != this.control_.Playlist.Current.Source)
                    {
                        this.Source = new Uri(this.control_.Playlist.Current.Source);
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
            this.control_.MediaSpeed += (sender, args) =>
                {
                    this.Speed = this.control_.Speed;
                    if (this.SpeedRequest != null)
                        this.SpeedRequest(this, EventArgs.Empty);
                };
        }

        public Uri Source { get; private set; }
        public double Speed { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OpenRequest;
        public event EventHandler PlayRequest;
        public event EventHandler PauseRequest;
        public event EventHandler StopRequest;
        public event EventHandler SpeedRequest;
    }
}
