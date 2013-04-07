
using System.Xml.Serialization;

namespace MyWmp.Models
{
    [XmlInclude(typeof(MediaPath))]
    public abstract class AMedia
    {
        public enum Type
        {
            Song,
            Video,
            Picture
        }

        public string Source { set; get; }
        public Type MediaType { set; get; }

        protected AMedia(string src, Type type)
        {
            Source = src;
            MediaType = type;
        }

        public abstract void Load();
    }

}
