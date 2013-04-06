using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Converters;
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
        private readonly Control control_;

        public LibraryViewModel()
        {
            AddPlaylistCommand = new ActionCommand(AddPlaylist);

            control_ = Control.Instance;

            library_ = Library.Instance;
            library_.Load();
            LibraryMusics = new ListCollectionView(library_.Sounds.ToArray());
            LibraryVideos = new ListCollectionView(library_.Videos.ToArray());
            LibraryPictures = new ArrayList(library_.Pictures.ToArray());
            Playlists = library_.Playlists;

            this.FilterCommand = new ActionCommand(OnFilter);
            this.GroupCommand = new ActionCommand(OnGroup);
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

        public void OnPlayLibraryPlaylist(int selectedPlaylist, int selectedItem)
        {
            var playlist = new Playlist {Name = Playlists[selectedPlaylist].Name};
            foreach (var media in LibraryPlaylist)
            {
                playlist.Add(media as AMedia);
            }
            playlist.Current = LibraryPlaylist.GetItemAt(selectedItem) as AMedia;
            control_.Playlist = playlist;
            control_.Play();
        }

        public void OnPlayLibraryVideos(int selectedIndex)
        {
            var playlist = new Playlist { Name = "Current Playlist" };
            playlist.Add(LibraryVideos.GetItemAt(selectedIndex) as AMedia);
            playlist.Current = LibraryVideos.GetItemAt(selectedIndex) as AMedia;
            control_.Playlist = playlist;
            control_.Play();
        }

        public void OnPlayLibraryMusics(int selectedIndex)
        {
            var playlist = new Playlist { Name = "Current Playlist" };
            foreach (var media in LibraryMusics)
            {
                playlist.Add(media as AMedia);
            }
            playlist.Current = LibraryMusics.GetItemAt(selectedIndex) as AMedia;
            control_.Playlist = playlist;
            control_.Play();
        }

        private string Translate(string library)
        {
            switch (library)
            {
                case "Music":
                    return "LibraryMusics";
                case "Video":
                    return "LibraryVideos";
                case "Picture":
                    return "LibraryPictures";
                default:
                    return "LibraryMusics";
            }
        }

        private void OnFilter(object o)
        {
            var param = o as LibraryConverterParam;
            if (param != null && param.Filter)
                ((ListCollectionView) this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).Filter =
                    a => ((string) a.GetType().GetProperty(param.Sender).GetValue(a, null)).Contains(param.Value);
            else if (param != null && param.Filter == false)
                ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).Filter = null;
        }

        private void OnGroup(object o)
        {
            var param = o as LibraryConverterParam;
            if (param != null && param.Group)
                ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.Add(new PropertyGroupDescription(param.Sender));
            else if (param != null && param.Group == false)
                ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.Remove(
                     ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.First(a => a.ToString() == (new PropertyGroupDescription(param.Sender)).ToString()));
        }

        public ICommand FilterCommand { get; private set; }
        public ICommand GroupCommand { get; private set; }
    }
}
