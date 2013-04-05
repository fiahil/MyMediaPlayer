
namespace MyWmp.Models
{
    abstract class AMedia
    {
        public enum Type
        {
            Song,
            Video,
            Picture
        }

        public string Source { private set; get; }
        public Type MediaType { private set; get; }

        protected AMedia(string src, Type type)
        {
            Source = src;
            MediaType = type;
        }

        public abstract void Load();
    }

}
