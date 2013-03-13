using System;
using System.Windows.Controls;

namespace MyWmp.Items
{
    class MediaPlaylistElement : MediaElement
    {
        public Playlist Playlist;

        public new void Play()
        {
            if (Playlist != null)
            {
                Source = new Uri(Playlist.Current.Src);
            }
            base.Play();
        }
    }
}
