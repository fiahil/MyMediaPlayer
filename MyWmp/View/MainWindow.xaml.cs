﻿using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using MyWmp.Models;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        private readonly Control control_;

        public MainWindow()
        {
            InitializeComponent();
            control_ = Control.Instance;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnFullScreenCommand(object sender, MouseButtonEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).FullScreenCommand.CanExecute(null))
                ((MainWindowViewModel)this.DataContext).FullScreenCommand.Execute(null);

        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            var soundExt = new string[] { ".mp3" };
            var videoExt = new string[] { ".mp4" };
            var pictureExt = new string[] { ".jpg" };
            var loader = new Loader() { FileExtension = new string[] { ".mp3", ".mp4", ".jpg" } };
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
                    if (soundExt.Contains(ext))
                    {
                        item = new Song(media);
                    }
                    else if (videoExt.Contains(ext))
                        item = new Video(media);
                    else if (pictureExt.Contains(ext))
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
            //this.ResizeMode = this.ResizeMode == ResizeMode.NoResize ? ResizeMode.CanResizeWithGrip : ResizeMode.NoResize;
          /*  ne fonctionne toujours pas :(
            if (((MainWindowViewModel)this.DataContext).RestoreCommand.CanExecute(null))
               ((MainWindowViewModel)this.DataContext).RestoreCommand.Execute(null);
           */
        }

        private void OnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SwitchButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string) this.SwitchButton.Content == "/Resources/Minimize.png")
            {
                this.MediaLayout.Visibility = Visibility.Visible;
                this.ControlLayout.Visibility = Visibility.Visible;
                this.LibraryLayout.Visibility = Visibility.Collapsed;
                this.SwitchButton.Content = "/Resources/Maximize.png";
            }
            else
            {
                this.MediaLayout.Visibility = Visibility.Collapsed;
                this.ControlLayout.Visibility = Visibility.Collapsed;
                this.LibraryLayout.Visibility = Visibility.Visible;
                this.SwitchButton.Content = "/Resources/Minimize.png";
            }
        }
    }
}
