using System;
using System.Collections;

namespace MyWmp.Models
{
    class Playlist
    {
        private readonly Random random_ = new Random();
        private readonly ArrayList songs_;
        private readonly ArrayList shuffleList_;
        public AMedia Current
        {
            set { if (songs_.Contains(value)) current_ = value; }
            get { return current_; }
        }

        public bool Shuffle
        {
            set
            {
                if (value)
                {
                    RandomList(shuffleList_);
                    if (Current != null)
                    {
                        shuffleList_.Remove(Current);
                        shuffleList_.Insert(0, Current);
                    }
                }
                shuffle_ = value;
            }
            get { return shuffle_; }
        }

        public bool Repeat;
        private bool shuffle_;
        private AMedia current_;

        private void RandomList(IList list, uint b = 0, uint n = 0)
        {
            if (n == 0)
                n = (uint) list.Count - b;
            for (var i = b; i < n; ++i)
            {
                var a = random_.Next((int)b, (int)n);
                var c = random_.Next((int)b, (int)n);
                if (a != c)
                {
                    var tmp = list[a];
                    list[a] = list[c];
                    list[c] = tmp;
                }
            }
        }

        private void ResetIndex()
        {
            Current = null;
        }

        public Playlist()
        {
            songs_ = new ArrayList();
            shuffleList_ = new ArrayList();
            ResetIndex();
            Shuffle = false;
            Repeat = false;
        }

        public void Add(AMedia s)
        {
            songs_.Add(s);
            shuffleList_.Add(s);
            RandomList(shuffleList_);
            if (songs_.Count == 1)
            {
                Current = s;
            }
        }

        public void Remove(AMedia index)
        {
            songs_.Remove(index);
            shuffleList_.Remove(index);
            if (songs_.Count == 0)
                ResetIndex();
            else if (Current == index)
                Next();
        }

        public void RemoveAll()
        {
            songs_.Clear();
            shuffleList_.Clear();
            ResetIndex();
        }

        public void Next()
        {
            if (songs_.Count == 0) return;
            int index;
            if (Shuffle)
            {
                index = shuffleList_.IndexOf(Current) + 1;
                if (index >= shuffleList_.Count)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = 0;
                }
                index = songs_.IndexOf(shuffleList_[index]);
            }
            else
            {
                index = songs_.IndexOf(Current) + 1;
                if (index >= songs_.Count)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = 0;
                }
            }
            Current = songs_[index] as AMedia;
        }

        public void Prev()
        {
            if (songs_.Count == 0) return;
            int index;
            if (Shuffle)
            {
                index = shuffleList_.IndexOf(Current) - 1;
                if (index < 0)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = shuffleList_.Count - 1;
                }
                index = songs_.IndexOf(shuffleList_[index]);
            }
            else
            {
                index = songs_.IndexOf(Current) - 1;
                if (index < 0)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = songs_.Count - 1;
                }
            }
            Current = songs_[index] as AMedia;
        }
    }
}
