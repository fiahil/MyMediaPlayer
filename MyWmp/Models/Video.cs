
using System;
using System.IO;
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
        public TimeSpan Duration { private set; get; }
        public String Genre { private set; get; }
        public int Height { private set; get; }
        public int Width { private set; get; }
        public String Extension { private set; get; }

        public Video(string src)
            : base(src, Type.Video)
        {
            Load();
        }

        public override sealed void Load()
        {
            try
            {
                var file = File.Create(Src);
                Title = file.Tag.Title;
                if (Title == null)
                    Title = Path.GetFileNameWithoutExtension(Src);
                Genre = String.Join(";", file.Tag.Genres);
                if (file.Properties.MediaTypes != MediaTypes.None)
                    Duration = file.Properties.Duration;
                Height = file.Properties.VideoHeight;
                Width = file.Properties.VideoWidth;
                Extension = Path.GetExtension(Src).Remove(0, 1).ToLower();
            }
            catch (UnsupportedFormatException)
            {
            }
        }
    }
}
