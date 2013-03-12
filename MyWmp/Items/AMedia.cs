
namespace MyWmp.Items
{
    abstract class AMedia
    {
        public enum Type
        {
            Song,
            Video,
        }

        public string Src { private set; get; }
        public Type MediaType { private set; get; }

        protected AMedia(string src, Type type)
        {
            Src = src;
            MediaType = type;
        }

        public abstract void Load();
    }
}
