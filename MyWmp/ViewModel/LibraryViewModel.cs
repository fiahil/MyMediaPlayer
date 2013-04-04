using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    internal class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ListCollectionView LibraryMusics { get; private set; }
        public ListCollectionView LibraryVideos { get; private set; }
        public ArrayList LibraryPictures { get; private set; }
        public ObservableCollection<Playlist> Playlists { get; private set; }
        public ListCollectionView LibraryPlaylist { get; private set; }

        private readonly Library library_;

        public LibraryViewModel()
        {
            AddPlaylistCommand = new ActionCommand(AddPlaylist);

            library_ = Library.Instance;
            library_.Load();
            LibraryMusics = new ListCollectionView(library_.Sounds.ToArray());
            LibraryMusics.GroupDescriptions.Add(new PropertyGroupDescription("Artist"));
            LibraryMusics.GroupDescriptions.Add(new PropertyGroupDescription("Album"));
            LibraryVideos = new ListCollectionView(library_.Videos.ToArray());
            LibraryPictures = new ArrayList(library_.Pictures.ToArray());
            Playlists = library_.Playlists;
        }

        public void AddPlaylist()
        {
            library_.AddPlaylist();
            PropertyChanged(this, new PropertyChangedEventArgs("Playlists"));
        }

        public ICommand AddPlaylistCommand { get; private set; }

        public void OnPlaylistChanged(int selectedIndex)
        {
            LibraryPlaylist = new ListCollectionView(Playlists[selectedIndex].ToArray());
            PropertyChanged(this, new PropertyChangedEventArgs("LibraryPlaylist"));
        }
    }
}
