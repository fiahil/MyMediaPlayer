using System.Windows;
using System.Windows.Controls;
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

        private void ArtistValue_OnTextChanged(object sender, TextChangedEventArgs e)
        {
           if (this.ArtistFilter.IsChecked.HasValue && this.ArtistGroup.IsChecked.HasValue && this.ArtistFilter.IsChecked.Value)
               this.libraryViewModel_.FilterCommand.Execute(new LibraryConverterParam
                   {
                       Sender = "Artist",
                       Filter = this.ArtistFilter.IsChecked.Value,
                       Group = this.ArtistGroup.IsChecked.Value,
                       Value = this.ArtistValue.Text
                   });
        }
    }
}
