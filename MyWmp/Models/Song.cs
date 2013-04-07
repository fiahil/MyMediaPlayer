using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using TagLib;
using File = TagLib.File;

namespace MyWmp.Models
{
    class Song : AMedia
    {
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Artist { private set; get; }
        public String Album { private set; get; }
        public String Genre { private set; get; }
        public String Year { private set; get; }
        public String Track { private set; get; }
        public String Duration { private set; get; }
        public String Extension { private set; get; }
        public BitmapImage AlbumArt { private set; get; }

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
                var file = File.Create(Source);
                Title = file.Tag.Title;
                if (file.Tag.AlbumArtists.Length != 0)
                    Artist = file.Tag.AlbumArtists[0];
                else if (file.Tag.Artists.Length != 0)
                    Artist = file.Tag.Artists[0];
                Album = file.Tag.Album;
                Genre = String.Join(";", file.Tag.Genres);
                Year = file.Tag.Year.ToString();
                Track = file.Tag.Track.ToString();
                if (file.Properties.MediaTypes != MediaTypes.None)
                    Duration = file.Properties.Duration.ToString(@"mm\:ss");
                var extension = Path.GetExtension(Source);
                if (extension != null) Extension = extension.Remove(0, 1).ToLower();
                if (file.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                    var tmp = Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(100, 100, null, IntPtr.Zero);
                    AlbumArt = new BitmapImage();
                    AlbumArt.BeginInit();
                    var memoryStream = new MemoryStream();
                    tmp.Save(memoryStream, ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    AlbumArt.StreamSource = memoryStream;
                    AlbumArt.EndInit();
                }
                else
                {
                    AlbumArt = new BitmapImage(new Uri("/Resources/Music.png", UriKind.Relative));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
