using System;
using TagLib;

namespace MyWmp.Models
{
    class Song : AMedia
    {
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Artist { private set; get; }
        public String Album { private set; get; }
        public String Genre { private set; get; }
        public uint Year { private set; get; }
        public uint Track { private set; get; }
        public TimeSpan Duration { private set; get; }

        public Song(String src) : base(src, Type.Song)
        {
            Title = Unknown;
            Artist = Unknown;
            Album = Unknown;
            Genre = Unknown;
            Load();
        }

        public override sealed void Load()
        {
            try
            {
                var file = File.Create(Src);
                Title = file.Tag.Title;
                Artist = String.Join(";", file.Tag.AlbumArtists);
                Album = file.Tag.Album;
                Genre = String.Join(";", file.Tag.Genres);
                Year = file.Tag.Year;
                Track = file.Tag.Track;
                if (file.Properties.MediaTypes != MediaTypes.None)
                    Duration = file.Properties.Duration;
            }
            catch (UnsupportedFormatException)
            {
            }
        }
    }
}
