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
        public List<String> MediaPath { private set; get; }

        public Loader()
        {
            MediaPath = new List<string>();
            FileExtension = new string[] { };
        }

        private void LoadRec(String dir)
        {
            try
            {
                var files = Directory.GetFiles(dir, "*.*").Where(s => FileExtension.Contains(new FileInfo(s).Extension.ToLower())).ToArray();
                var subDir = Directory.GetDirectories(dir);

                foreach (var sound in files)
                {
                    MediaPath.Add(sound);
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
            try
            {
                MediaPath.Clear();
                if (File.Exists(Root) && FileExtension.Contains(new FileInfo(Root).Extension.ToLower())) 
                    MediaPath.Add(Root);
                else
                    LoadRec(Root);
            }
            catch (Exception e)
            {
            }
        }
    }
}
