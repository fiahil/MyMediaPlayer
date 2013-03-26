using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    class ControlViewModel : INotifyPropertyChanged
    {
        private readonly Control control_;

        public ControlViewModel()
        {
            this.control_ = Control.Instance;

            this.TimeEnabled = false;
            this.TimeNotifier = 100;

            this.PlayPauseNotifier = "/Resources/Play.png";
            this.StopNotifier = "/Resources/Stop.png";
            this.PrevNotifier = "/Resources/Prev.png";
            this.NextNotifier = "/Resources/Next.png";
            this.ShuffleNotifier = "/Resources/Shuffle.png";
            this.RepeatAllNotifier = "/Resources/RepeatAll.png";

            this.PlayPauseCommand = new ActionCommand(OnPlayPause);
            this.StopCommand = new ActionCommand(OnStop);
            this.PrevCommand = new ActionCommand(OnPrev);
            this.NextCommand = new ActionCommand(OnNext);
            this.ShuffleCommand = new ActionCommand(OnShuffle);
            this.RepeatAllCommand = new ActionCommand(OnRepeatAll);
        }

        private void OnRepeatAll()
        {
            this.control_.RepeatAll();
            this.RefreshPlayPause();
            this.PropertyChanged(this, new PropertyChangedEventArgs("RepeatAllNotifier"));
        }

        private void OnShuffle()
        {
            this.control_.Shuffle();
            this.RefreshPlayPause();
            this.PropertyChanged(this, new PropertyChangedEventArgs("ShuffleNotifier"));
        }

        private void OnNext()
        {
            this.control_.Next();
            this.RefreshPlayPause();
            this.PropertyChanged(this, new PropertyChangedEventArgs("NextNotifier"));
        }

        private void OnPrev()
        {
            this.control_.Prev();
            this.RefreshPlayPause();
            this.PropertyChanged(this, new PropertyChangedEventArgs("PrevNotifier"));
        }

        private void OnStop()
        {
            this.control_.Stop();
            this.RefreshPlayPause();
            this.PropertyChanged(this, new PropertyChangedEventArgs("StopNotifier"));
        }

        private void RefreshPlayPause()
        {
            this.PlayPauseNotifier = this.control_.IsPlaying ? "/Resources/Pause.png" : "/Resources/Play.png";
            this.TimeEnabled = this.control_.IsPlaying;
            this.PropertyChanged(this, new PropertyChangedEventArgs("TimeEnabled"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("PlayPauseNotifier"));
        }

        private void OnPlayPause()
        {
            if (this.control_.IsPlaying)
                this.control_.Pause();
            else
                this.control_.Play();

            this.RefreshPlayPause();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int TimeNotifier { get; private set; }
        public bool TimeEnabled { get; private set; }

        public string PlayPauseNotifier { get; private set; }
        public string StopNotifier { get; private set; }
        public string PrevNotifier { get; private set; }
        public string NextNotifier { get; private set; }
        public string ShuffleNotifier { get; private set; }
        public string RepeatAllNotifier { get; private set; }

        public ICommand PlayPauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand PrevCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand ShuffleCommand { get; private set; }
        public ICommand RepeatAllCommand { get; private set; }
    }
}
