using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for LibraryLayout.xaml
    /// </summary>
    public partial class LibraryLayout : UserControl
    {
        public LibraryLayout()
        {
            InitializeComponent();
        }

        private void ListPlaylist_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlaylistChanged(ListPlaylist.SelectedIndex);
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
