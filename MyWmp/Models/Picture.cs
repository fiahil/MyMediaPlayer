using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using File = TagLib.File;

namespace MyWmp.Models
{
    class Picture : AMedia
    {
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Height { private set; get; }
        public String Width { private set; get; }
        public String Year { get; private set; }
        public String Extension { private set; get; }
        public BitmapImage AlbumArt { private set; get; }

        public Picture(string src) : base(src, Type.Picture)
        {
            Load();
        }

        public override sealed void Load()
        {
            try
            {
                var file = File.Create(Source);
                Title = Path.GetFileNameWithoutExtension(Source);
                Year = file.Tag.Year.ToString(CultureInfo.InvariantCulture);
                Height = file.Properties.PhotoHeight.ToString(CultureInfo.InvariantCulture);
                Width = file.Properties.PhotoWidth.ToString(CultureInfo.InvariantCulture);
                var extension = Path.GetExtension(Source);
                if (extension != null) Extension = extension.Remove(0, 1).ToLower();
                AlbumArt = new BitmapImage(new Uri(Source));
            }
            catch (Exception e)
            {
            }
        }
    }
}
