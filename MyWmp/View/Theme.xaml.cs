using System.Windows;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for Theme.xaml
    /// </summary>
    public partial class Theme
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void Theme_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.PopupTheme.IsOpen = this.Visibility == Visibility.Visible;
        }
    }
}
