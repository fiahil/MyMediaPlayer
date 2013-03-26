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
                    this.Source = this.control_.Playlist;

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

        public Playlist Source { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler PlayRequest;
        public event EventHandler PauseRequest;
        public event EventHandler StopRequest;
    }
}
