using System.Collections;

namespace MyWmp.Items
{
    class Playlist
    {
        private readonly ArrayList _songs;
        private int _currentIndex;
        public AMedia Current { private set; get; }
        public bool Shuffle;
        public bool Repeat;

        private void ResetIndex()
        {
            Current = null;
            _currentIndex = - 1;
        }

        public Playlist()
        {
            _songs = new ArrayList();
            ResetIndex();
            Shuffle = false;
            Repeat = false;
        }

        public void Add(AMedia s)
        {
            _songs.Add(s);
            if (_songs.Count == 1)
            {
                Current = s;
                _currentIndex = 0;
            }
        }

        public void Remove(AMedia index)
        {
            _songs.Remove(index);
            if (!_songs.Contains(index))
                Next();
        }

        public void RemoveAll()
        {
            _songs.Clear();
            ResetIndex();
        }

        public void Next()
        {
            if (_songs.Count == 0) return;
            if (Shuffle)
            {
                
            }
            else
            {
                ++_currentIndex;
                if (_currentIndex >= _songs.Count)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    _currentIndex = 0;
                }
            }
            Current = _songs[_currentIndex] as AMedia;
        }

        public void Prev()
        {
            if (_songs.Count == 0) return;
            if (Shuffle)
            {
            }
            else
            {
                --_currentIndex;
                if (_currentIndex < 0)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    _currentIndex = _songs.Count - 1;
                }
            }
            Current = _songs[_currentIndex] as AMedia;
        }
    }
}
