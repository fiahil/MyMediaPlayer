using System;
using System.Windows;
using Microsoft.Win32;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for MenuLayout.xaml
    /// </summary>
    public partial class MenuLayout
    {
        public MenuLayout()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var f = new OpenFileDialog
                    {
                        CheckFileExists = true,
                        ReadOnlyChecked = true
                    };
                f.ShowDialog();
                if (!String.IsNullOrEmpty(f.FileName))
                {
                    var s = f.OpenFile();

                    if (((MenuViewModel) this.DataContext).OpenCommand.CanExecute(s))
                        ((MenuViewModel) this.DataContext).OpenCommand.Execute(s);
                }
            }
            catch (Exception exception)
            {
                ((MenuViewModel) this.DataContext).FailCommand.Execute(exception.Message);
            }
        }
    }
}
