
using System;
using System.Xml.Serialization;
using TagLib;

namespace MyWmp.Models
{
    class Video : AMedia
    {
        [XmlIgnore]
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public TimeSpan Duration { private set; get; }
        public String Genre { private set; get; }

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
                Genre = String.Join(";", file.Tag.Genres);
                if (file.Properties.MediaTypes != MediaTypes.None)
                    Duration = file.Properties.Duration;
            }
            catch (UnsupportedFormatException)
            {
            }
        }
    }
}
