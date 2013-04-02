
using System;

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

        public bool IsLoaded { get; private set; }

        private Library()
        {
            Sounds = new Playlist();
            Videos = new Playlist();
            Pictures = new Playlist();
        }

        public void Load()
        {
            IsLoaded = false;
            Sounds.RemoveAll();
            var loader = new Loader { Root = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), FileExtension = new[] { ".mp3" } };
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
            IsLoaded = true;
        }
    }
}
