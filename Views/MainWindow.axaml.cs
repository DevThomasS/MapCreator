using Avalonia.Controls;
using Avalonia.Interactivity;
using MapCreator.Views;

namespace MapCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateMapType(MapType.Geographical);
        }

        private void OnGeoClick(object sender, RoutedEventArgs e)
        {
            UpdateMapType(MapType.Geographical);
        }

        private void OnPolClick(object sender, RoutedEventArgs e)
        {
            UpdateMapType(MapType.Political);
        }

        private void UpdateMapType(MapType mapType)
        {
            var geoPanel = this.FindControl<GeographicalMenu>("GeographicalMenu");
            if (geoPanel is null) return;
            var polPanel = this.FindControl<PoliticalMenu>("PoliticalMenu");
            if (polPanel is null) return;

            geoPanel.IsVisible = mapType == MapType.Geographical;
            polPanel.IsVisible = mapType == MapType.Political;
            MapTypeText.Text = $"Current Map: {mapType}";
        }
    }
}
