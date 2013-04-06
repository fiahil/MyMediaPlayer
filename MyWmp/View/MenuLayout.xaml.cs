using System.Linq;
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

                var f = new OpenFileDialog
                    {
                        CheckFileExists = true,
                        ReadOnlyChecked = true,
                        Multiselect = true
                    };
                f.ShowDialog();
                if (f.FileNames.Any())
                {
                    if (((MenuViewModel) this.DataContext).OpenCommand.CanExecute(f.FileNames))
                        ((MenuViewModel) this.DataContext).OpenCommand.Execute(f.FileNames);
                }
        }
    }
}
