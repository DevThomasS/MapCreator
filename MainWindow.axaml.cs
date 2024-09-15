using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MapCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Attach event handlers for each ListBox selection.
            TraversableLandListBox.SelectionChanged += OnSelectionChanged;
            RiversListBox.SelectionChanged += OnSelectionChanged;
            ImpassableLandListBox.SelectionChanged += OnSelectionChanged;
            SeaListBox.SelectionChanged += OnSelectionChanged;

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
            var geoPanel = this.FindControl<ScrollViewer>(MapType.Geographical.ToString());
            if (geoPanel is null) return;
            var polPanel = this.FindControl<ScrollViewer>(MapType.Political.ToString());
            if (polPanel is null) return;

            geoPanel.IsVisible = mapType == MapType.Geographical;
            polPanel.IsVisible = mapType == MapType.Political;
            MapTypeText.Text = $"Current Map: {mapType}";
        }

        // Method to handle selection change in any of the ListBox controls
        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox listBox) return;
            var selectedItem = listBox.SelectedItem;

            // Clear selection in all listboxes
            if (listBox != TraversableLandListBox)
                TraversableLandListBox.SelectedItem = null;

            if (listBox != RiversListBox)
                RiversListBox.SelectedItem = null;

            if (listBox != ImpassableLandListBox)
                ImpassableLandListBox.SelectedItem = null;

            if (listBox != SeaListBox)
                SeaListBox.SelectedItem = null;
            
            listBox.SelectedItem = selectedItem;
        }

        private void OnCreateNewRegionClick(object? sender, RoutedEventArgs e)
        {
            var newRegionFields = this.FindControl<StackPanel>("NewRegionFields");
            if (newRegionFields is not null)
                newRegionFields.IsVisible = !newRegionFields.IsVisible;
        }

        private void OnSaveRegionClick(object sender, RoutedEventArgs e)
        {
            OnCreateNewRegionClick(sender, e);
            //TODO: High Priority. Data from previous region is carried over to new region (e.g. Capital Name).
            // Proceed with region creation logic
        }
    }
}
