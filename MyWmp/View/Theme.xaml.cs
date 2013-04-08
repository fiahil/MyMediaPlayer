using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using Xceed.Wpf.Toolkit;

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

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            this.PopupTheme.HorizontalOffset += e.HorizontalChange;
            this.PopupTheme.VerticalOffset += e.VerticalChange;
        }

        private void PopupTheme_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Thumb.RaiseEvent(e);
        }

        private void OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            MainWindow.Instance.Resources.MergedDictionaries[0][((ColorPicker)sender).Tag.ToString()] = new SolidColorBrush(e.NewValue);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
