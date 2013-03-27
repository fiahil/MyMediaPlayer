using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    class PlaylistSerializer
    {
        public void Serialize(Playlist p)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            using (StreamWriter wr = new StreamWriter(p.Name + ".xml"))
                xs.Serialize(wr, p);
        }

        public Playlist DeSerialize(string name)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            Playlist p;
            using (StreamReader rd = new StreamReader(name + ".xml"))
                return p = xs.Deserialize(rd) as Playlist;
        }
    }

    class SettingsSerializer
    {
        public void Serialize(Settings s)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            using (StreamWriter wr = new StreamWriter("settings.xml"))
                xs.Serialize(wr, s);
        }

        public Settings DeSerialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            Settings s;
            using (StreamReader rd = new StreamReader("settings.xml"))
                return s = xs.Deserialize(rd) as Settings;
        }
    }
}
