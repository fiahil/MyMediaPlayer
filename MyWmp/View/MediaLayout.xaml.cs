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

            this.viewModel_.PlayRequest += (sender, args) =>
                {
                    this.Media.Playlist = this.viewModel_.Source;
                    this.Media.Play();
                };
            this.viewModel_.PauseRequest += (sender, args) => this.Media.Pause();
            this.viewModel_.StopRequest += (sender, args) => this.Media.Stop();
        }
    }
}
