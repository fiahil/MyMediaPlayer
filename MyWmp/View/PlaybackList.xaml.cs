using System.Windows.Controls;
using System.Windows.Input;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for PlaybackList.xaml
    /// </summary>
    public partial class PlaybackList : UserControl
    {
        public PlaybackList()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((PlaybackListViewModel)DataContext).OnPlay(((ListView)sender).SelectedIndex);
        }
    }
}
