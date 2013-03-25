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

        public Playlist UnSerialize(string name)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Playlist));
            Playlist p;
            using (StreamReader rd = new StreamReader(name + ".xml"))
                return p = xs.Deserialize(rd) as Playlist;
        }
    }
}
