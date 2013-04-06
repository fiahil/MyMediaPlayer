using System.Reflection;
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
            ((LibraryViewModel) DataContext).OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, ((DataGrid)sender).SelectedIndex);
        }

        private void Videos_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LibraryViewModel) DataContext).OnPlayLibraryVideos(((DataGrid)sender).SelectedIndex);
        }

        private void Musics_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LibraryViewModel)DataContext).OnPlayLibraryMusics(((DataGrid)sender).SelectedIndex);
        }
    }
}
