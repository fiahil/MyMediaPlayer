using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MyWmp.Models;
using MyWmp.ViewModel;
using Control = MyWmp.Models.Control;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        public static MainWindow Instance;
        private readonly Control control_;
        private readonly Settings settings_;

        public MainWindow()
        {
            InitializeComponent();
            control_ = Control.Instance;
            settings_ = Settings.Instance;
            Instance = this;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnFullScreenCommand(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            if (Grid.GetColumn(MediaLayout) != 0)
            {
                Grid.SetRow(MediaLayout, 0);
                Grid.SetRowSpan(MediaLayout, 2);
                Grid.SetColumn(MediaLayout, 0);
                Grid.SetColumnSpan(MediaLayout, 6);
                this.MediaLayout.Margin = new Thickness(0, 0, 0, -5);
            }
            else
            {
                Grid.SetRow(MediaLayout, 1);
                Grid.SetRowSpan(MediaLayout, 1);
                Grid.SetColumn(MediaLayout, 1);
                Grid.SetColumnSpan(MediaLayout, 4);
                this.MediaLayout.Margin = new Thickness(0, 0, 0, 52);
            }

        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            var allExtensions = new List<string>();
            allExtensions.AddRange(settings_.MusicExtensions);
            allExtensions.AddRange(settings_.VideoExtensions);
            allExtensions.AddRange(settings_.PictureExtensions);
            var loader = new Loader() { FileExtension = allExtensions.ToArray() };
            var playlist = control_.Playlist ?? new Playlist() { Name = "Current Playlist" };
            var first = false;
            foreach (var file in fileList)
            {
                loader.Root = file;
                loader.Load();
                foreach (var media in loader.MediaPath)
                {
                    AMedia item = null;
                    var ext = new FileInfo(media).Extension.ToLower();
                    if (settings_.MusicExtensions.Contains(ext))
                    {
                        item = new Song(media);
                    }
                    else if (settings_.VideoExtensions.Contains(ext))
                        item = new Video(media);
                    else if (settings_.PictureExtensions.Contains(ext))
                    {
                        item = new Picture(media);
                    }
                    if (item != null)
                    {
                        playlist.Add(item);
                        if (!first)
                        {
                            playlist.Current = item;
                            first = true;
                        }
                    }
                }
            }
            control_.Playlist = playlist;
            control_.Play();
        }

        private void OnMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).MinimizeCommand.CanExecute(null))
                ((MainWindowViewModel)this.DataContext).MinimizeCommand.Execute(null);
        }

        private void OnRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.Width = (this.Width == SystemParameters.VirtualScreenWidth) ? (850) : (SystemParameters.VirtualScreenWidth);
            this.Height = (this.Height == SystemParameters.VirtualScreenHeight - 40) ? (480) : (SystemParameters.VirtualScreenHeight - 40);
            this.Top = (this.Top == 0) ? ((SystemParameters.VirtualScreenHeight - 480) / 2) : (0);
            this.Left = (this.Left == 0) ? ((SystemParameters.VirtualScreenWidth - 850) / 2) : (0);
        }

        private void OnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SwitchButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string) this.SwitchButton.Content == "/Resources/Retract.png")
            {
                this.MediaLayout.Visibility = Visibility.Visible;
                this.ControlLayout.Visibility = Visibility.Visible;
                this.LibraryLayout.Visibility = Visibility.Collapsed;
                this.SwitchButton.Content = "/Resources/Deploy.png";
            }
            else
            {
                this.MediaLayout.Visibility = Visibility.Collapsed;
                this.ControlLayout.Visibility = Visibility.Collapsed;
                this.LibraryLayout.Visibility = Visibility.Visible;
                this.SwitchButton.Content = "/Resources/Retract.png";
            }
        }

        private void SwitchButton2_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string)this.SwitchButton2.Content == "/Resources/Retract.png")
            {
                this.PlaybackList.Visibility = Visibility.Visible;

                this.SwitchButton2.Content = "/Resources/Deploy.png";
            }
            else
            {
                this.PlaybackList.Visibility = Visibility.Collapsed;

                this.SwitchButton2.Content = "/Resources/Retract.png";
            }

        }
    }
}
