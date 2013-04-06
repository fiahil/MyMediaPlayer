using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    internal class PlaybackListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Control control_;

        public string Name { get; private set; }
        public ObservableCollection<AMedia> Playlist { get; private set; }

        public PlaybackListViewModel()
        {
            control_ = Control.Instance;

            this.control_.MediaPlay += (sender, args) =>
                {
                    Name = control_.Playlist.Name;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    Playlist.Clear();
                    foreach (var media in control_.Playlist.ToArray())
                    {
                        Playlist.Add(media as AMedia);
                    }
                    PropertyChanged(this, new PropertyChangedEventArgs("Playlist"));
                };
            Playlist = new ObservableCollection<AMedia>(new List<AMedia>());
        }
    }
}
