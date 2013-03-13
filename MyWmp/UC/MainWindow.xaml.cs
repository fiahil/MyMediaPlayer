namespace MyWmp.UC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            new Services.LoaderService {FileExtension = null, Root = "."}.Load();
        }
    }
}
