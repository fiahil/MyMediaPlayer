using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    class Picture : AMedia
    {
        [XmlIgnore]
        public static readonly String Unknown = "Unknown";
        public String Title { private set; get; }
        public String Author { private set; get; }

        public Picture(string src) : base(src, Type.Picture)
        {
            Load();
        }

        public override sealed void Load()
        {
        }
    }
}
