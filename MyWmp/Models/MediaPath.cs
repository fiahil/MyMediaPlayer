
namespace MyWmp.Models
{
    public class MediaPath : AMedia
    {
        public MediaPath() : base("", Type.Video)
        {
            
        }

        public MediaPath(string src, Type type) : base(src, type)
        {
        }

        public override void Load()
        {
            
        }
    }
}
