using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyWmp.Models
{
    class Loader
    {
        public String Root;
        public String[] FileExtension;
        public List<String> SoundPath { private set; get; }

        public Loader()
        {
            SoundPath = new List<string>();
            FileExtension = new string[] {};
        }

        private void LoadRec(String dir)
        {
            try
            {
                var files = Directory.GetFiles(dir, "*.*").Where(s => FileExtension.Contains(new FileInfo(s).Extension.ToLower())).ToArray();
                var subDir = Directory.GetDirectories(dir);

                foreach (var sound in files)
                {
                    SoundPath.Add(sound);
                }
                foreach (var s in subDir)
                {
                    LoadRec(s);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        public void Load()
        {
            SoundPath.Clear();
            LoadRec(Root);
        }
    }
}
