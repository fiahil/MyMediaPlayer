using System.Windows;
using System.Windows.Controls;

namespace MyWmp.View
{
    /// <summary>
    /// Logique d'interaction pour OpenStream.xaml
    /// </summary>
    public partial class OpenStream : UserControl
    {
        public OpenStream()
        {
            InitializeComponent();
        }

        private void OpenStream_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Popop.IsOpen = this.Visibility == Visibility.Visible;
        }

        private void ConfirmStream_OnClick(object sender, RoutedEventArgs e)
        {
            
            this.Visibility = Visibility.Collapsed;
        }
    }
}
