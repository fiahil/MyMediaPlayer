
using System;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    class Video : AMedia
    {
        [XmlIgnore]
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public TimeSpan Duration { private set; get; }

        public Video(string src) : base(src, Type.Video)
        {
            Load();
        }

        public override sealed void Load()
        {
        }
    }
}
