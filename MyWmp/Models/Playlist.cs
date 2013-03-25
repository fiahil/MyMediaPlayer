using System;
using System.Collections;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    class Playlist
    {
        [XmlIgnore]
        private readonly Random random_ = new Random();
        [XmlArray("Medias")]
        private readonly ArrayList medias_;
        [XmlIgnore]
        private readonly ArrayList shuffleList_;

        public string Name { set; get; }
        [XmlIgnore]
        public AMedia Current
        {
            set { if (medias_.Contains(value)) current_ = value; }
            get { return current_; }
        }
        [XmlIgnore]
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
        [XmlIgnore]
        public bool Repeat;
        [XmlIgnore]
        private bool shuffle_;
        [XmlIgnore]
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
            medias_ = new ArrayList();
            shuffleList_ = new ArrayList();
            ResetIndex();
            Shuffle = false;
            Repeat = false;
            Name = "New Playlist";
        }

        public void Add(AMedia s)
        {
            medias_.Add(s);
            shuffleList_.Add(s);
            RandomList(shuffleList_);
            if (medias_.Count == 1)
            {
                Current = s;
            }
        }

        public void Remove(AMedia index)
        {
            medias_.Remove(index);
            shuffleList_.Remove(index);
            if (medias_.Count == 0)
                ResetIndex();
            else if (Current == index)
                Next();
        }

        public void RemoveAll()
        {
            medias_.Clear();
            shuffleList_.Clear();
            ResetIndex();
        }

        public void Next()
        {
            if (medias_.Count == 0) return;
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
                index = medias_.IndexOf(shuffleList_[index]);
            }
            else
            {
                index = medias_.IndexOf(Current) + 1;
                if (index >= medias_.Count)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = 0;
                }
            }
            Current = medias_[index] as AMedia;
        }

        public void Prev()
        {
            if (medias_.Count == 0) return;
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
                index = medias_.IndexOf(shuffleList_[index]);
            }
            else
            {
                index = medias_.IndexOf(Current) - 1;
                if (index < 0)
                {
                    if (!Repeat)
                    {
                        ResetIndex();
                        return;
                    }
                    index = medias_.Count - 1;
                }
            }
            Current = medias_[index] as AMedia;
        }
    }
}
