using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MapCreator.Core;
using Newtonsoft.Json;

namespace MapCreator.Views
{
    public partial class PoliticalMenu : UserControl
    {
        public PoliticalMenu()
        {
            InitializeComponent();
            LoadPanelData<Religions>();
            LoadPanelData<Cultures>();
            LoadListBoxData<Keywords>();
        }

        private void OnCreateNewRegionClick(object? sender, RoutedEventArgs e)
        {
            var newRegionFields = this.FindControl<StackPanel>("NewRegionFields");
            if (newRegionFields != null)
                newRegionFields.IsVisible = !newRegionFields.IsVisible;
        }

        private void OnSaveRegionClick(object sender, RoutedEventArgs e)
        {
            OnCreateNewRegionClick(sender, e);
            //TODO: High Priority. Data from previous region is carried over to new region (e.g. Capital Name).
            // Proceed with region creation logic
        }

        private async void LoadPanelData<T>() where T : BaseEntity
        {
            var typeName = typeof(T).Name;
            var dataList = await LoadDataFromFileAsync<T>(typeName);
            var panel = this.FindControl<StackPanel>(typeName);
            if (panel == null) return;
          
            foreach (var data in dataList)
            {
                var textBlock = new TextBlock
                {
                    Text = $"{data.Description}: 0%"
                };

                var slider = new Slider
                {
                    Name = data.Name,
                    Minimum = 0,
                    Maximum = 100,
                    TickFrequency = 1,
                    IsSnapToTickEnabled = true,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
                };

                slider.ValueChanged += (sender, e) =>
                {
                    textBlock.Text = $"{data.Description}: {slider.Value}%";
                };

                panel.Children.Add(textBlock);
                panel.Children.Add(slider);
            }
        }

        private async void LoadListBoxData<T>() where T : BaseEntity
        {
            var typeName = typeof(T).Name;
            var keywords = await LoadDataFromFileAsync<T>(typeName);
            var listBox = this.FindControl<ListBox>(typeName);
            if (listBox == null) return;

            foreach (var keyword in keywords)
            {
                var item = new ListBoxItem
                {
                    Name = keyword.Name,
                    Content = keyword.Description,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
                };

                listBox.Items.Add(item);
            }
        }

        private static async Task<List<T>> LoadDataFromFileAsync<T>(string type) where T : BaseEntity
        {
            var filePath = $"Resources/{type}.json";
            if (!File.Exists(filePath)) return [];

            var json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? [];
        }
    }
}
