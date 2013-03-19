using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace MyWmp.ViewModel
{
    class ControlViewModel : INotifyPropertyChanged
    {
        public ControlViewModel()
        {
            this.Play = new ActionCommand(() =>
                {
                    this.Text = "/Resources/Pause.png";
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                });
            this.Text = "/Resources/Play.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text { get; private set; }
        public ICommand Play { get; private set; }
    }
}
