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
    }
}
