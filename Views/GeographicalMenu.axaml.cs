using Avalonia.Controls;

namespace MapCreator.Views
{
  public partial class GeographicalMenu : UserControl
  {
    public GeographicalMenu()
    {
      InitializeComponent();

      // Attach event handlers for the ListBoxes in GeographicalMenu.
      TraversableLandListBox.SelectionChanged += OnSelectionChanged;
      RiversListBox.SelectionChanged += OnSelectionChanged;
      ImpassableLandListBox.SelectionChanged += OnSelectionChanged;
      SeaListBox.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
      if (sender is not ListBox listBox) return;
      var selectedItem = listBox.SelectedItem;

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
  }
}
