using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MyWmp.Models;

namespace MyWmp.ViewModel
{
    class MenuViewModel
    {
        private Control control_;

        public MenuViewModel()
        {
            this.control_ = Control.Instance;

            this.OpenCommand = new ActionCommand(OnOpen);
            this.QuitCommand = new ActionCommand(OnQuit);
            this.FailCommand = new ActionCommand(OnFail);
            this.PlayCommand = new ActionCommand(OnPlay);
            this.PauseCommand = new ActionCommand(OnPause);
            this.StopCommand = new ActionCommand(OnStop);
            this.PrevCommand = new ActionCommand(OnPrev);
            this.NextCommand = new ActionCommand(OnNext);
            this.ShuffleCommand = new ActionCommand(OnShuffle);
            this.RepeatCommand = new ActionCommand(OnRepeat);
            this.SpeedUpCommand = new ActionCommand(OnSpeedUp);
            this.SpeedDownCommand = new ActionCommand(OnSpeedDown);
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

        private void OnOpen(object stream)
        {
            throw new System.NotImplementedException();
        }

        private void OnQuit()
        {
            Application.Current.Shutdown();
        }

        private void OnFail(object message)
        {
            throw new System.NotImplementedException();
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        public ICommand FailCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand PrevCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand ShuffleCommand { get; private set; }
        public ICommand RepeatCommand { get; private set; }
        public ICommand SpeedUpCommand { get; private set; }
        public ICommand SpeedDownCommand { get; private set; }
    }
}
