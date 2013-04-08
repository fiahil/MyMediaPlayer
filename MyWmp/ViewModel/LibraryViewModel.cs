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
        public ListCollectionView LibraryPictures { get; private set; }
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
            LibraryPictures = new ListCollectionView(library_.Pictures.ToArray());
            LibraryPlaylist = new ListCollectionView(library_.Playlists);
            Playlists = library_.Playlists;

            this.FilterCommand = new ActionCommand(OnFilter);
            this.GroupCommand = new ActionCommand(OnGroup);
        }

        public void DeletePlaylist(int selectedIndex)
        {
            library_.DeletePlaylist(selectedIndex);
            PropertyChanged(this, new PropertyChangedEventArgs("Playlists"));
        }

        public void AddPlaylist()
        {
            library_.AddPlaylist();
            PropertyChanged(this, new PropertyChangedEventArgs("Playlists"));
        }

        public ICommand AddPlaylistCommand { get; private set; }

        public void OnPlaylistChanged(int selectedIndex)
        {
            if (selectedIndex != -1)
                LibraryPlaylist = new ListCollectionView(Playlists[selectedIndex].ToArray());
            PropertyChanged(this, new PropertyChangedEventArgs("LibraryPlaylist"));
        }

        public void OnPlayLibraryPlaylist(int selectedPlaylist, int selectedItem)
        {
            if (selectedPlaylist == -1 || selectedPlaylist == -1)
                return;
            var playlist = new Playlist {Name = Playlists[selectedPlaylist].Name};
            foreach (var media in LibraryPlaylist)
            {
                playlist.Add(media as AMedia);
            }
            if (selectedItem != -1 && LibraryPlaylist.Count > 0)
            {
                playlist.Current = LibraryPlaylist.GetItemAt(selectedItem) as AMedia;
                control_.Playlist = playlist;
                control_.Play();
            }
        }

        public void OnPlayLibraryVideos(int selectedIndex)
        {
            var playlist = new Playlist { Name = "Current Playlist" };
            playlist.Add(LibraryVideos.GetItemAt(selectedIndex) as AMedia);
            if (selectedIndex != -1 && LibraryVideos.Count > 0)
            {
                playlist.Current = LibraryVideos.GetItemAt(selectedIndex) as AMedia;
                control_.Playlist = playlist;
                control_.Play();
            }
        }

        public void OnPlayLibraryMusics(int selectedIndex)
        {
            var playlist = new Playlist { Name = "Current Playlist" };
            foreach (var media in LibraryMusics)
            {
                playlist.Add(media as AMedia);
            }
            if (selectedIndex != -1 && LibraryMusics.Count > 0)
            {
                playlist.Current = LibraryMusics.GetItemAt(selectedIndex) as AMedia;
                control_.Playlist = playlist;
                control_.Play();
            }
        }

        public void OnPlayLibraryPictures(int selectedIndex)
        {
            var playlist = new Playlist { Name = "Current Playlist" };
            foreach (var media in LibraryPictures)
            {
                playlist.Add(media as AMedia);
            }
            if (selectedIndex != -1 && LibraryPictures.Count > 0)
            {
                playlist.Current = LibraryPictures.GetItemAt(selectedIndex) as AMedia;
                control_.Playlist = playlist;
                control_.Play();
            }
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
                case "Playlist":
                    return "LibraryPlaylist";
                default:
                    return "LibraryMusics";
            }
        }

        private void OnFilter(object o)
        {
            try
            {
                var param = o as LibraryConverterParam;
                if (param != null && param.Filter)
                    ((ListCollectionView) this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).Filter =
                        a => ((string) a.GetType().GetProperty(param.Sender).GetValue(a, null)).Contains(param.Value);
                else if (param != null && param.Filter == false)
                    ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).Filter = null;
            }
            catch (Exception e)
            {
            }
        }

        private void OnGroup(object o)
        {
            try
            {
                var param = o as LibraryConverterParam;
                if (param != null && param.Group)
                    ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.Add(new PropertyGroupDescription(param.Sender));
                else if (param != null && param.Group == false)
                    ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.Remove(
                        ((ListCollectionView)this.GetType().GetProperty(Translate(param.Library)).GetValue(this, null)).GroupDescriptions.First(a => a.ToString() == (new PropertyGroupDescription(param.Sender)).ToString()));
            }
            catch (Exception e)
            {
            }
        }

        public ICommand FilterCommand { get; private set; }
        public ICommand GroupCommand { get; private set; }

        public void AddItemIntoPlaylist(int selectedPlaylist, string library, int selectedItem)
        {
            if (selectedPlaylist == -1 || selectedItem == -1)
                return;
            library_.AddItemIntoPlaylist(selectedPlaylist, ((ListCollectionView)this.GetType().GetProperty(Translate(library)).GetValue(this, null)).GetItemAt(selectedItem) as AMedia);
            LibraryPlaylist = new ListCollectionView(Playlists[selectedPlaylist].ToArray());
            if (control_.Playlist.Name.CompareTo(Playlists[selectedPlaylist].Name) == 0)
                control_.Playlist = Playlists[selectedPlaylist];
            PropertyChanged(this, new PropertyChangedEventArgs("LibraryPlaylist"));
        }

        public void DeleteItemFromPlaylist(int selectedPlaylist, int selectedItem)
        {
            if (selectedPlaylist == -1 || selectedItem == -1)
                return;
            library_.DeleteItemFromPlaylist(selectedPlaylist, selectedItem);
            LibraryPlaylist = new ListCollectionView(Playlists[selectedPlaylist].ToArray());
            if (control_.Playlist.Name.CompareTo(Playlists[selectedPlaylist].Name) == 0)
                control_.Playlist = Playlists[selectedPlaylist];
            PropertyChanged(this, new PropertyChangedEventArgs("LibraryPlaylist"));
        }

        public void OnSetName(int selectedPlaylist, string name)
        {
            library_.SetNameFromPlaylist(selectedPlaylist, name);
        }
    }
}
