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
        }

        public Playlist Source { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler PlayRequest;
    }
}
