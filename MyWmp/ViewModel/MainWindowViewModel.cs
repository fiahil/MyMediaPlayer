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
            this.WidthNotifier = (this.WidthNotifier == SystemParameters.VirtualScreenWidth) ? (850) : (SystemParameters.VirtualScreenWidth);
            this.HeightNotifier = (this.HeightNotifier == SystemParameters.VirtualScreenHeight - 40) ? (450) : (SystemParameters.VirtualScreenHeight - 40);
            this.TopNotifier = (this.TopNotifier == 0) ? ((SystemParameters.VirtualScreenHeight - 480) / 2) : (0);
            this.LeftNotifier = (this.LeftNotifier == 0) ? ((SystemParameters.VirtualScreenWidth - 850) / 2) : (0);
            this.PropertyChanged(this, new PropertyChangedEventArgs("WidthNotifier"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("HeightNotifier"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("TopNotifier"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("LeftNotifier"));       
        }


        public double WidthNotifier { get; set; }
        public double HeightNotifier { get; set; }
        public double TopNotifier { get; set; }
        public double LeftNotifier { get; set; }
        public WindowState WindowStateNotifier { get; set; }

        public ICommand FullScreenCommand { get; private set; }
        public ICommand MinimizeCommand { get; private set; }
        public ICommand RestoreCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
