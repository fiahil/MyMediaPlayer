using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyWmp.Converters;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for LibraryLayout.xaml
    /// </summary>
    public partial class LibraryLayout
    {
        private readonly LibraryViewModel libraryViewModel_;

        public LibraryLayout()
        {
            InitializeComponent();
            this.libraryViewModel_ = new LibraryViewModel();
            this.DataContext = this.libraryViewModel_;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (
                ((CheckBox)
                 this.GetType()
                     .GetField((string) ((TextBox) sender).Tag + "Filter",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.HasValue &&
                ((CheckBox)
                 this.GetType()
                     .GetField((string) ((TextBox) sender).Tag + "Group",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.HasValue &&
                ((CheckBox)
                 this.GetType()
                     .GetField((string) ((TextBox) sender).Tag + "Filter",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.Value
                )
            {
                this.libraryViewModel_.FilterCommand.Execute(new LibraryConverterParam
                    {
                        Sender = (string) ((TextBox) sender).Tag,
                        Filter =
                            ((CheckBox)
                             this.GetType()
                                 .GetField((string) ((TextBox) sender).Tag + "Filter",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).IsChecked.Value,
                        Group =
                            ((CheckBox)
                             this.GetType()
                                 .GetField((string) ((TextBox) sender).Tag + "Group",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).IsChecked.Value,
                        Value =
                            ((TextBox)
                             this.GetType()
                                 .GetField((string) ((TextBox) sender).Tag + "Value",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).Text,
                        Library = ((string)
                                   ((GroupBox)
                                    this.GetType()
                                        .GetField((string) ((TextBox) sender).Tag + "GroupBox",
                                                  BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                        .GetValue(this)).Tag)
                    });
            }
        }

        private void ListPlaylist_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlaylistChanged(ListPlaylist.SelectedIndex);
        }

        private void Playlists_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, DataGridPlaylist.SelectedIndex);
        }

        private void Videos_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlayLibraryVideos(((DataGrid)sender).SelectedIndex);
        }

        private void Musics_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LibraryViewModel)DataContext).OnPlayLibraryMusics(((DataGrid)sender).SelectedIndex);
        }

        private void Playlist_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            ((LibraryViewModel)DataContext).DeletePlaylist(ListPlaylist.SelectedIndex);
        }

        private void Playlist_Play_OnClick(object sender, RoutedEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, 0);
        }

        private void Playlist_Rename_OnClick(object sender, RoutedEventArgs e)
        {
            var textbox =
                (TextBox)
                ((Grid) ((Label) ((ContextMenu) ((MenuItem) sender).Parent).PlacementTarget).Parent).Children[1];
            textbox.Visibility = Visibility.Visible;
            textbox.SelectAll();
            textbox.Focus();
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((TextBox) sender).Visibility = Visibility.Collapsed;
            }
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Visibility = Visibility.Collapsed;
        }

        private void LibraryPlaylist_Play_OnClick(object sender, RoutedEventArgs e)
        {
            ((LibraryViewModel)DataContext).OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, DataGridPlaylist.SelectedIndex);
        }

        private void LibraryPlaylist_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            ((LibraryViewModel) DataContext).DeleteItemFromPlaylist(ListPlaylist.SelectedIndex, DataGridPlaylist.SelectedIndex);
        }

        private void DataGridPlaylist_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var viewModel = (LibraryViewModel) DataContext;
            switch (e.Key)
            {
                    case Key.Delete:
                    viewModel.DeleteItemFromPlaylist(ListPlaylist.SelectedIndex, DataGridPlaylist.SelectedIndex);
                    break;
                    case Key.Enter:
                    viewModel.OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, DataGridPlaylist.SelectedIndex);
                    break;
                    case Key.Down:
                    DataGridPlaylist.SelectedIndex = (DataGridPlaylist.SelectedIndex + 1) % DataGridPlaylist.Items.Count;
                    break;
                    case Key.Up:
                    --DataGridPlaylist.SelectedIndex;
                    if (DataGridPlaylist.SelectedIndex < 0)
                        DataGridPlaylist.SelectedIndex = DataGridPlaylist.Items.Count - 1;
                    break;
            }

        }

        private void DataGridVideos_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var viewModel = (LibraryViewModel)DataContext;
            var datagrid = (DataGrid) sender;
            switch (e.Key)
            {
                case Key.Enter:
                    viewModel.OnPlayLibraryVideos(datagrid.SelectedIndex);
                    break;
                case Key.Down:
                    datagrid.SelectedIndex = (datagrid.SelectedIndex + 1) % datagrid.Items.Count;
                    break;
                case Key.Up:
                    --datagrid.SelectedIndex;
                    if (datagrid.SelectedIndex < 0)
                        datagrid.SelectedIndex = datagrid.Items.Count - 1;
                    break;
            }
        }

        private void MusicDatagrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var viewModel = (LibraryViewModel)DataContext;
            var datagrid = (DataGrid)sender;
            switch (e.Key)
            {
                case Key.Enter:
                    viewModel.OnPlayLibraryMusics(datagrid.SelectedIndex);
                    break;
                case Key.Down:
                    datagrid.SelectedIndex = (datagrid.SelectedIndex + 1) % datagrid.Items.Count;
                    break;
                case Key.Up:
                    --datagrid.SelectedIndex;
                    if (datagrid.SelectedIndex < 0)
                        datagrid.SelectedIndex = datagrid.Items.Count - 1;
                    break;
            }
        }
    }
}
