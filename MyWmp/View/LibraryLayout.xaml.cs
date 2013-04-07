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
            var library = "";
            switch (((string)((TextBox)sender).Tag)[0])
            {
                case 'M':
                    library = "Music";
                    break;
                case 'V':
                    library = "Video";
                    break;
                case 'P':
                    library = "Picture";
                    break;
                case 'L':
                    library = "Playlist";
                    break;
            }
            var tag = ((string) ((TextBox) sender).Tag).Substring(1);

            if (
                ((CheckBox)
                 this.GetType()
                     .GetField(library + tag + "Filter",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.HasValue &&
                ((CheckBox)
                 this.GetType()
                     .GetField(library + tag + "Group",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.HasValue &&
                ((CheckBox)
                 this.GetType()
                     .GetField(library + tag + "Filter",
                               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                     .GetValue(this)).IsChecked.Value
                )
            {
                this.libraryViewModel_.FilterCommand.Execute(new LibraryConverterParam
                    {
                        Sender = tag,
                        Filter =
                            ((CheckBox)
                             this.GetType()
                                 .GetField(library + tag + "Filter",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).IsChecked.Value,
                        Group =
                            ((CheckBox)
                             this.GetType()
                                 .GetField(library + tag + "Group",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).IsChecked.Value,
                        Value =
                            ((TextBox)
                             this.GetType()
                                 .GetField(library + tag + "Value",
                                           BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                 .GetValue(this)).Text,
                        Library = ((string)
                                   ((GroupBox)
                                    this.GetType()
                                        .GetField(library + tag + "GroupBox",
                                                  BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                        .GetValue(this)).Tag)
                    });
            }
        }

        private void ListPlaylist_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            libraryViewModel_.OnPlaylistChanged(ListPlaylist.SelectedIndex);
        }

        private void Playlists_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            libraryViewModel_.OnPlayLibraryPlaylist(ListPlaylist.SelectedIndex, ((DataGrid)sender).SelectedIndex);
        }

        private void Videos_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            libraryViewModel_.OnPlayLibraryVideos(((DataGrid)sender).SelectedIndex);
        }

        private void Musics_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            libraryViewModel_.OnPlayLibraryMusics(((DataGrid)sender).SelectedIndex);
        }
    }
}
