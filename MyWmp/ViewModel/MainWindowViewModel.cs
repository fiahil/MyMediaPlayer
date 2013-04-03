using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.Expression.Interactivity.Core;

namespace MyWmp.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            this.WindowStateNotifier = WindowState.Normal;
            this.FullScreenCommand = new ActionCommand(OnFullScreenCommand);
            this.MinimizeCommand = new ActionCommand(OnMinimizeCommand);
            this.RestoreCommand = new ActionCommand(OnRestoreCommand);
        }

        private void OnFullScreenCommand()
        {
            this.WindowStateNotifier = this.WindowStateNotifier == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            this.PropertyChanged(this, new PropertyChangedEventArgs("WindowStateNotifier"));
        }


        private void OnMinimizeCommand()
        {
            this.WindowStateNotifier = WindowState.Minimized;
            this.PropertyChanged(this, new PropertyChangedEventArgs("WindowStateNotifier"));
        }

        private void OnRestoreCommand()
        {
            this.WindowStateNotifier = this.WindowStateNotifier == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            this.PropertyChanged(this, new PropertyChangedEventArgs("WindowStateNotifier"));
        }


        public WindowState WindowStateNotifier { get; set; }

        public ICommand FullScreenCommand { get; private set; }
        public ICommand MinimizeCommand { get; private set; }
        public ICommand RestoreCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
