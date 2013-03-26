
namespace MyWmp.Models
{
    class Video : AMedia
    {
        public Video(string src) : base(src, Type.Video)
        {
            Load();
        }

        public override sealed void Load()
        {
        }
    }
}
