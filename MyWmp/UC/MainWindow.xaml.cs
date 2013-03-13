namespace MyWmp.UC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MainLayout.Children.Add(new ControlLayout());
        }
    }
}
