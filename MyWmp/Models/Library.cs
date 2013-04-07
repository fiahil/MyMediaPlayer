
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

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

        private Library()
        {
            Sounds = new Playlist();
            Videos = new Playlist();
            Pictures = new Playlist();
            Playlists = new ObservableCollection<Playlist>();
        }

        public void Load()
        {
            IsLoaded = false;
            Sounds.RemoveAll();
            var loader = new Loader
                {
                    Root = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                    FileExtension = new[] { ".mp3" }
                };
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Sounds.Add(new Song(media));
            }
            Videos.RemoveAll();
            loader.Root = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            loader.FileExtension = new[] {".mp4"};
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Videos.Add(new Video(media));
            }
            Pictures.RemoveAll();
            loader.Root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            loader.FileExtension = new[] {".png"};
            loader.Load();
            foreach (var media in loader.MediaPath)
            {
                Pictures.Add(new Picture(media));
            }
            loader.Root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyPlaylists";
            loader.FileExtension = new[] {".xml"};
            loader.Load();
            foreach (var playlist in loader.MediaPath)
            {
                var newInstance = PlaylistSerializer.DeSerialize(playlist);
                newInstance.LoadFromPath();
                Playlists.Add(newInstance);
            }
            IsLoaded = true;
        }

        public void AddPlaylist()
        {
            var tmp = new Playlist();
            tmp.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyPlaylists\\" + tmp.Name + ".xml";
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
            Playlists[selectedPlaylist].Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                               "\\MyPlaylists\\" + Playlists[selectedPlaylist].Name + ".xml";
            PlaylistSerializer.Serialize(Playlists[selectedPlaylist]);
        }
    }
}
