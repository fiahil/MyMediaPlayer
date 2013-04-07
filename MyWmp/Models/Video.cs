﻿
using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using TagLib;
using File = TagLib.File;

namespace MyWmp.Models
{
    class Video : AMedia
    {
        [XmlIgnore]
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Duration { private set; get; }
        public String Year { private set; get; }
        public String Genre { private set; get; }
        public String Height { private set; get; }
        public String Width { private set; get; }
        public String Extension { private set; get; }
        public BitmapImage AlbumArt { private set; get; }

        public Video(string src)
            : base(src, Type.Video)
        {
            Load();
        }

        public override sealed void Load()
        {
            try
            {
                var file = File.Create(Source);
                Title = file.Tag.Title;
                if (Title == null)
                    Title = Path.GetFileNameWithoutExtension(Source);
                Year = file.Tag.Year.ToString();
                Genre = String.Join(";", file.Tag.Genres);
                if (file.Properties.MediaTypes != MediaTypes.None)
                    Duration = file.Properties.Duration.ToString(@"hh\:mm\:ss");
                Height = file.Properties.VideoHeight.ToString();
                Width = file.Properties.VideoWidth.ToString();
                Extension = Path.GetExtension(Source).Remove(0, 1).ToLower();
                AlbumArt = new BitmapImage(new Uri("/Resources/Video.png", UriKind.Relative));
            }
            catch (Exception)
            {
            }
        }
    }
}
