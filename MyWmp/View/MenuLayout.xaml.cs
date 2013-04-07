using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MyWmp.ViewModel;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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
                if (((MenuViewModel)this.DataContext).OpenCommand.CanExecute(f.FileNames))
                    ((MenuViewModel)this.DataContext).OpenCommand.Execute(f.FileNames);
            }
        }

        private void MenuItem_OnOpenDir(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            var file = new string[2];
            file[0] = fbd.SelectedPath;
            if (((MenuViewModel)this.DataContext).OpenCommand.CanExecute(file))
                ((MenuViewModel)this.DataContext).OpenCommand.Execute(file);
        }
    }
}
