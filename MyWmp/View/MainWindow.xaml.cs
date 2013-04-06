﻿using System;
using System.Windows;
using System.Windows.Input;
using MyWmp.ViewModel;

namespace MyWmp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
            foreach (var s in fileList)
            {
                // En attente du open.
                Console.Write(s);
            }
        }

        private void OnMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (((MainWindowViewModel)this.DataContext).MinimizeCommand.CanExecute(null))
                ((MainWindowViewModel)this.DataContext).MinimizeCommand.Execute(null);
        }

        private void OnRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.Width = (this.Width == SystemParameters.VirtualScreenWidth) ? (this.MinWidth) : (SystemParameters.VirtualScreenWidth);
            this.Height = (this.Height == SystemParameters.VirtualScreenHeight - 40) ? (this.MinHeight) : (SystemParameters.VirtualScreenHeight - 40);
            this.Top = 0;
            this.Left = 0;
            //   if (((MainWindowViewModel)this.DataContext).RestoreCommand.CanExecute(null))
            //    ((MainWindowViewModel)this.DataContext).RestoreCommand.Execute(null);
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
