using System;
using System.IO;
using System.Xml.Serialization;
using File = TagLib.File;

namespace MyWmp.Models
{
    class Picture : AMedia
    {
        [XmlIgnore]
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Height { private set; get; }
        public String Width { private set; get; }
        public String Year { get; private set; }
        public String Extension { private set; get; }

        public Picture(string src) : base(src, Type.Picture)
        {
            Load();
        }

        public override sealed void Load()
        {
            var file = File.Create(Source);
            Title = Path.GetFileNameWithoutExtension(Source);
            Year = file.Tag.Year.ToString();
            Height = file.Properties.PhotoHeight.ToString();
            Width = file.Properties.PhotoWidth.ToString();
            var extension = Path.GetExtension(Source);
            if (extension != null) Extension = extension.Remove(0, 1).ToLower();
        }
    }
}
