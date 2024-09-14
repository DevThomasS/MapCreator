using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MapCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateMapType("Geographical");
        }

        private void OnGeoClick(object sender, RoutedEventArgs e)
        {
            UpdateMapType("Geographical");
        }

        private void OnPolClick(object sender, RoutedEventArgs e)
        {
            UpdateMapType("Political");
        }

        private void UpdateMapType(string mapType)
        {
            MapTypeText.Text = $"Current Map: {mapType}";
        }
    }
}
