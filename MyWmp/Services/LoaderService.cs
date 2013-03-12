using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyWmp.Services
{
    class LoaderService
    {
        public String Root;
        public String[] FileExtension;
        public List<String> SoundPath { private set; get; }

        public LoaderService()
        {
            SoundPath = new List<string>();
            FileExtension = new string[] {};
        }

        private void LoadRec(String dir)
        {
            try
            {
                String[] files = (string[]) Directory.GetFiles(dir, "*.*").Where(s => FileExtension.Contains(new FileInfo(s).Extension.ToLower())).ToArray();
                String[] subDir = Directory.GetDirectories(dir);

                foreach (string sound in files)
                {
                    SoundPath.Add(sound);
                }
                foreach (string s in subDir)
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
