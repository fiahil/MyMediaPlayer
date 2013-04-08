using System;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    public class Settings
    {
        [XmlIgnore]
        private static Settings instance_;
        [XmlIgnore]
        public static Settings Instance
        {
            get
            {
                if (instance_ == null)
                {
                    instance_ =
                        SettingsSerializer.DeSerialize();
                    if (instance_ == null)
                    {
                        instance_ = new Settings();
                        SettingsSerializer.Serialize(instance_);
                    }
                }
                return instance_;
            }
        }

        public Settings()
        {
            this.MusicLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            this.PictureLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.VideoLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            this.PlaylistPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyPlaylists";
            this.MusicExtensions = new[] { ".mp3", ".wav", ".wma" };
            this.VideoExtensions = new[] { ".avi", ".mp4", ".wmv" };
            this.PictureExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        }

        public String VideoLibPath { get; set; }
        public String MusicLibPath { get; set; }
        public String PictureLibPath { get; set; }
        public String PlaylistPath { get; set; }

        [XmlArray("MusicExtensions")]
        public String[] MusicExtensions { get; set; }
        [XmlArray("VideoExtensions")]

        public String[] VideoExtensions { get; set; }
        [XmlArray("PictureExtensions")]

        public String[] PictureExtensions { get; set; }
    }
}
