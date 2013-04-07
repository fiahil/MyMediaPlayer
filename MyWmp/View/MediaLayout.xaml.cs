using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for MediaLayout.xaml
    /// </summary>
    public partial class MediaLayout
    {
        private readonly MediaViewModel viewModel_;

        public MediaLayout()
        {
            InitializeComponent();

            this.viewModel_ = new MediaViewModel();
            this.DataContext = this.viewModel_;

            this.viewModel_.OpenRequest += (sender, args) => this.Media.Source = this.viewModel_.Source;
            this.viewModel_.PlayRequest += (sender, args) => this.Media.Play();
            this.viewModel_.PauseRequest += (sender, args) => this.Media.Pause();
            this.viewModel_.StopRequest += (sender, args) => this.Media.Stop();
            this.viewModel_.SpeedRequest += (sender, args) => this.Media.SpeedRatio = this.viewModel_.Speed;
        }

        private void TimeSlider_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            this.Media.Pause();
        }

        private void TimeSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.Media.Play();
        }
    }
}
