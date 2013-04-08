
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MyWmp.Models
{
    class Library
    {
        private static Library instance_;

        public static Library Instance
        {
            get { return instance_ ?? (instance_ = new Library()); }
        }

        public Playlist Sounds { get; private set; }
        public Playlist Videos { get; private set; }
        public Playlist Pictures { get; private set; }
        public ObservableCollection<Playlist> Playlists { get; private set; }

        public bool IsLoaded { get; private set; }
        private readonly Settings settings_;

        private Library()
        {
            Sounds = new Playlist();
            Videos = new Playlist();
            Pictures = new Playlist();
            Playlists = new ObservableCollection<Playlist>();
            settings_ = Settings.Instance;
        }

        public void Load()
        {
            IsLoaded = false;
            Sounds.RemoveAll();
            var loader = new Loader
                {
                    Root = settings_.MusicLibPath,
                    FileExtension = settings_.MusicExtensions
                };
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Sounds.Add(new Song(media));
            }
            Videos.RemoveAll();
            loader.Root = settings_.VideoLibPath;
            loader.FileExtension = settings_.VideoExtensions;
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Videos.Add(new Video(media));
            }
            Pictures.RemoveAll();
            loader.Root = settings_.PictureLibPath;
            loader.FileExtension = settings_.PictureExtensions;
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Pictures.Add(new Picture(media));
            }
            loader.Root = settings_.PlaylistPath;
            loader.FileExtension = new[] {".xml"};
            loader.Load();
            foreach (var newInstance in loader.MediaPath.Select(PlaylistSerializer.DeSerialize).Where(newInstance => newInstance != null))
            {
                newInstance.LoadFromPath();
                Playlists.Add(newInstance);
            }
            IsLoaded = true;
        }

        public void AddPlaylist()
        {
            var tmp = new Playlist();
            tmp.Path = settings_.PlaylistPath + "\\" + tmp.Name + ".xml";
            PlaylistSerializer.Serialize(tmp);
            Playlists.Add(tmp);
        }

        public void AddItemIntoPlaylist(int selectedPlaylist, AMedia media)
        {
            Playlists[selectedPlaylist].Add(media);
            PlaylistSerializer.Serialize(Playlists[selectedPlaylist]);
        }

        public void DeleteItemFromPlaylist(int selectedPlaylist, int selectedIndex)
        {
            Playlists[selectedPlaylist].Remove(Playlists[selectedPlaylist].ToArray()[selectedIndex] as AMedia);
            PlaylistSerializer.Serialize(Playlists[selectedPlaylist]);
        }

        public void DeletePlaylist(int selectedIndex)
        {
            File.Delete(Playlists[selectedIndex].Path);
            Playlists.RemoveAt(selectedIndex);
        }

        public void SetNameFromPlaylist(int selectedPlaylist, string name)
        {
            File.Delete(Playlists[selectedPlaylist].Path);
            Playlists[selectedPlaylist].Name = name;
            Playlists[selectedPlaylist].Path = settings_.PlaylistPath + "\\" + Playlists[selectedPlaylist].Name + ".xml";
            PlaylistSerializer.Serialize(Playlists[selectedPlaylist]);
        }
    }
}
