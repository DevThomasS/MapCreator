using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using MapCreator.Views;

namespace MapCreator
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new MainWindowViewModel();

      AttachedToVisualTree += async (sender, e) =>
      {
          await InitializePolMenuAsync();
          UpdateMapType(MapType.Geographical);
      };
    }

    private async Task InitializePolMenuAsync()
    {
        var polPanel = this.FindControl<PoliticalMenu>("PoliticalMenu");

        if (polPanel != null) await polPanel.InitializeAsync();
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

    private async Task OnImportBackgroundClickAsync(object sender, RoutedEventArgs e)
    {
      if (VisualRoot is Window window)
      {
        var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
          AllowMultiple = false,
          FileTypeFilter =
            [
              new FilePickerFileType("Image Files")
              {
                Patterns = ["*.png", "*.jpg", "*.jpeg", "*.bmp"]
              }
            ]
        });

        if (files.Any())
        {
          var selectedFile = files[0];
          await SetBackgroundImageAsync(selectedFile);
        }
      }
    }

    // You could create a method to set the background of your canvas:
    private async Task SetBackgroundImageAsync(IStorageFile selectedFile)
    {
      var stream = await selectedFile.OpenReadAsync();
      var bitmap = new Avalonia.Media.Imaging.Bitmap(stream);

      var imageControl = new Image
      {
        Source = bitmap,
        Stretch = Stretch.Fill,
        Opacity = 0.5
      };

      // Clear any existing children in the MapCanvas
      MapCanvas.Children.Clear();

      // Add the image to the canvas
      MapCanvas.Children.Add(imageControl);

      // Ensure the image covers the entire canvas
      Canvas.SetLeft(imageControl, 0);
      Canvas.SetTop(imageControl, 0);
      imageControl.Width = MapCanvas.Bounds.Width;
      imageControl.Height = MapCanvas.Bounds.Height;

      // Adjust the image when the canvas is resized
      MapCanvas.SizeChanged += (s, ev) =>
      {
        imageControl.Width = MapCanvas.Bounds.Width;
        imageControl.Height = MapCanvas.Bounds.Height;
      };
    }

    private void OnSetCanvasSizeClick(object sender, RoutedEventArgs e)
    {
      // TODO: Implement the logic to set the canvas size
    }

    private void OnExportMapClick(object sender, RoutedEventArgs e)
    {
      // TODO: Implement the logic to export the map
    }
  }
}
