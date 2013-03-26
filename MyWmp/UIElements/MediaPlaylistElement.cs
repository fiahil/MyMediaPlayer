using System;
using System.Windows.Controls;
using MyWmp.Models;

namespace MyWmp.UIElements
{
    class MediaPlaylistElement : MediaElement
    {
        public Playlist Playlist { get; set; }

        public new void Play()
        {
            if (Playlist != null)
            {
                var s = new Uri(Playlist.Current.Src, UriKind.Relative);
                if (Source == null || Source.OriginalString != s.OriginalString)
                    Source = s;
                base.Play();
            }
        }
    }
}
