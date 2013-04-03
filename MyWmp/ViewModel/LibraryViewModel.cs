using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    internal class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ListCollectionView LibraryMusics { get; private set; }
        public ListCollectionView LibraryVideos { get; private set; }
        public ArrayList LibraryPictures { get; private set; }
        public ArrayList Playlists { get; private set; }

        private readonly Library library_;

        public LibraryViewModel()
        {
            library_ = Library.Instance;
            library_.Load();
            LibraryMusics = new ListCollectionView(library_.Sounds.ToArray());
            LibraryMusics.GroupDescriptions.Add(new PropertyGroupDescription("Artist"));
            LibraryMusics.GroupDescriptions.Add(new PropertyGroupDescription("Album"));
            LibraryVideos = new ListCollectionView(library_.Videos.ToArray());
            LibraryPictures = new ArrayList(library_.Pictures.ToArray());
            Playlists = new ArrayList {new Playlist()};
        }

    }
}
