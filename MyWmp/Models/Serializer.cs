using System;
using System.IO;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    class PlaylistSerializer
    {
        public static void Serialize(Playlist p)
        {
            try
            {
                var xs = new XmlSerializer(typeof (Playlist));
                if (!Directory.Exists(Path.GetDirectoryName(p.Path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(p.Path));
                using (var wr = new StreamWriter(p.Path))
                    xs.Serialize(wr, p);
            }
            catch (Exception)
            {
                
            }

        }

        public static Playlist DeSerialize(string name)
        {
            try
            {
                var xs = new XmlSerializer(typeof (Playlist));
                using (var rd = new StreamReader(name))
                    return xs.Deserialize(rd) as Playlist;
            }
            catch (Exception)
            {

            }
            return null;
        }
    }

    class SettingsSerializer
    {
        public static void Serialize(Settings s)
        {
            try
            {
                var xs = new XmlSerializer(typeof (Settings));
                using (var wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\settings.xml"))
                    xs.Serialize(wr, s);
            }
            catch (Exception e)
            {
                
            }
        }

        public static Settings DeSerialize()
        {
            try
            {
                var xs = new XmlSerializer(typeof (Settings));
                using (var rd = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\settings.xml"))
                    return xs.Deserialize(rd) as Settings;
            }
            catch (Exception)
            {
                
            }
            return null;
        }
    }
}
