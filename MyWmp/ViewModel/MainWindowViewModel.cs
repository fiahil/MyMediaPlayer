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
            this.WidthNotifier = 850;
            this.HeightNotifier = 480;
            this.TopNotifier = (SystemParameters.VirtualScreenHeight - 480) / 2;
            this.LeftNotifier = (SystemParameters.VirtualScreenWidth - 850) / 2;
            this.WindowStateNotifier = WindowState.Normal;
            this.FullScreenCommand = new ActionCommand(OnFullScreenCommand);
            this.MinimizeCommand = new ActionCommand(OnMinimizeCommand);
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


        public double WidthNotifier { get; private set; }
        public double HeightNotifier { get; private set; }
        public double TopNotifier { get; private set; }
        public double LeftNotifier { get; private set; }
        public WindowState WindowStateNotifier { get; private set; }

        public ICommand FullScreenCommand { get; private set; }
        public ICommand MinimizeCommand { get; private set; }
        public ICommand RestoreCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
