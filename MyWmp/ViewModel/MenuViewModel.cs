using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    class MenuViewModel
    {
        private readonly Control control_;
        private readonly Settings settings_;

        public MenuViewModel()
        {
            this.control_ = Control.Instance;
            this.settings_ = Settings.Instance;

            this.OpenCommand = new ActionCommand(OnOpen);
            this.QuitCommand = new ActionCommand(OnQuit);
            this.PlayCommand = new ActionCommand(OnPlay);
            this.PauseCommand = new ActionCommand(OnPause);
            this.StopCommand = new ActionCommand(OnStop);
            this.PrevCommand = new ActionCommand(OnPrev);
            this.NextCommand = new ActionCommand(OnNext);
            this.ShuffleCommand = new ActionCommand(OnShuffle);
            this.RepeatCommand = new ActionCommand(OnRepeat);
            this.SpeedUpCommand = new ActionCommand(OnSpeedUp);
            this.SpeedDownCommand = new ActionCommand(OnSpeedDown);
            this.SpeedResetCommand = new ActionCommand(OnSpeedReset);
        }

        private void OnSpeedReset()
        {
            this.control_.SpeedReset();
        }

        private void OnSpeedDown()
        {
            this.control_.SpeedDown();
        }

        private void OnSpeedUp()
        {
            this.control_.SpeedUp();
        }

        private void OnRepeat()
        {
            this.control_.RepeatAll();
        }

        private void OnShuffle()
        {
            this.control_.Shuffle();
        }

        private void OnNext()
        {
            this.control_.Next();
        }

        private void OnPrev()
        {
            this.control_.Prev();
        }

        private void OnStop()
        {
            this.control_.Stop();
        }

        private void OnPause()
        {
            this.control_.Pause();
        }

        private void OnPlay()
        {
            this.control_.Play();
        }

        private void OnOpen(object fileNames)
        {
            var allExtensions = new ArrayList();
            allExtensions.AddRange(settings_.MusicExtensions);
            allExtensions.AddRange(settings_.VideoExtensions);
            allExtensions.AddRange(settings_.PictureExtensions);
            var loader = new Loader { FileExtension = allExtensions.ToArray() as string[] };
            var playlist = control_.Playlist ?? new Playlist {Name = "Current Playlist"};
            var strings = fileNames as string[];
            if (strings != null)
            {
                foreach (var file in strings)
                {
                    loader.Root = file;
                    loader.Load();
                    foreach (var media in loader.MediaPath)
                    {
                        AMedia item = null;
                        var ext = new FileInfo(media).Extension.ToLower();
                        if (settings_.MusicExtensions.Contains(ext))
                        {
                            item = new Song(media);
                        }
                        else if (settings_.VideoExtensions.Contains(ext))
                            item = new Video(media);
                        else if (settings_.PictureExtensions.Contains(ext))
                        {
                            item = new Picture(media);
                        }
                        if (item != null)
                            playlist.Add(item);
                    }
                }
            }
            control_.Playlist = playlist;
        }

        private void OnQuit()
        {
            Application.Current.Shutdown();
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand PrevCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand ShuffleCommand { get; private set; }
        public ICommand RepeatCommand { get; private set; }
        public ICommand SpeedUpCommand { get; private set; }
        public ICommand SpeedDownCommand { get; private set; }
        public ICommand SpeedResetCommand { get; private set; }
    }
}
