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
            get { return instance_ ?? (instance_ = new Settings()); }
        }

        public Settings()
        {
            this.MusicLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            this.PictureLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.VideoLibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            this.PlaylistPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyPlaylists";
        }

        public String VideoLibPath { get; set; }
        public String MusicLibPath { get; set; }
        public String PictureLibPath { get; set; }
        public String PlaylistPath { get; set; }
        [XmlArray("Extensions")]
        public String[] Extensions { get; set; }
    }
}
