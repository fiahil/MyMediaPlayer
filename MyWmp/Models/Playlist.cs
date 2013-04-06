using System;
using System.Collections;
using System.Xml.Serialization;

namespace MyWmp.Models
{
    [XmlRoot]
    class Playlist
    {
        [XmlIgnore]
        private readonly Random random_ = new Random();
        [XmlArray("Medias")]
        private readonly ArrayList medias_;
        [XmlIgnore]
        private readonly ArrayList shuffleList_;

        [XmlAttribute("Name")]
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
        [XmlIgnore] public bool RepeatAll;
        [XmlIgnore]
        private bool shuffle_;
        [XmlIgnore]
        private AMedia current_;

        private void RandomList(IList list, int b = 0, int n = 0)
        {
            if (n == 0)
                n = list.Count - b;
            for (var i = b; i < n; ++i)
            {
                var a = random_.Next(b, n);
                var c = random_.Next(b, n);
                if (a != c)
                {
                    var tmp = list[a];
                    list[a] = list[c];
                    list[c] = tmp;
                }
            }
        }

        public Playlist()
        {
            medias_ = new ArrayList();
            shuffleList_ = new ArrayList();
            current_ = null;
            Shuffle = false;
            RepeatAll = false;
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

        public void Add(IList list)
        {
            medias_.AddRange(list);
            shuffleList_.AddRange(list);
            RandomList(shuffleList_);
            if (medias_.Count == 1)
                Current = (AMedia) medias_[0];
        }

        public void Remove(AMedia index)
        {
            medias_.Remove(index);
            shuffleList_.Remove(index);
            if (medias_.Count == 0)
                current_ = null;
            else if (Current == index)
                Next();
        }

        public void RemoveAll()
        {
            medias_.Clear();
            shuffleList_.Clear();
            current_ = null;
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
                    if (!RepeatAll)
                    {
                        current_ = null;
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
                    if (!RepeatAll)
                    {
                        current_ = null;
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
                    if (!RepeatAll)
                    {
                        current_ = null;
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
                    if (!RepeatAll)
                    {
                        current_ = null;
                        return;
                    }
                    index = medias_.Count - 1;
                }
            }
            Current = medias_[index] as AMedia;
        }

        public void Reset()
        {
            if (medias_.Count > 0)
                current_ = medias_[0] as AMedia;
        }

        public Object[] ToArray()
        {
            return medias_.ToArray();
        }
    }
}
