using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Buying_Guide.Models;
using Buying_Guide.ViewModel;

namespace Buying_Guide.View
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        private List<string> _products = new List<string>();
        private readonly CatalogModel _model = new CatalogModel();
        private readonly AdminWindow _adminWindow;
        private List<string> _images;
        private List<string> _descriptions;
        private List<int> _counts;
        private List<Decimal> _prices;
        public Catalog( )
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _prices = _model.GetPrice();
            _descriptions = _model.GetDiscription();
            _images = _model.GetImages();
            _products = _model.GetProducts();
            _counts = _model.GetCount();
            for(int i = 0; i < _products.Count; i++)
                StackPanel.Children.Add(GetGrid(i));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private UIElement GetIsHaving(int i)
        {
            Label label = new Label();
            if (_counts[i] != 0)
                label.Content = _prices[i].ToString();
            else
                label.Content = "Нет в наличии";
            return label;
        }

        private UIElement GetLabel(int i)
        {
            Label label = new Label()
            {
                Content = _descriptions[i]

            };
            return label;
        }

        private void Search_String(object sender, TextChangedEventArgs e)
        {

        }
        
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateShop update = new UpdateShop(Title);
            update.Show();
        }

        private Grid GetGrid(int i)
        {
            GC.Collect();
            Grid grid = new Grid
            {
                Height = 100,
                Width = 720,
                Background = Brushes.AliceBlue,
                Margin = new Thickness(20, 6, 6, 6)
            };
            grid.Children.Add(GetImageGrid(_images[i]));
            grid.Children.Add(GetIsHaving(i));
            grid.Children.Add(GetLabel(i));
            return grid;
        }

        private Grid GetImageGrid(string address)
        {
            Grid grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(4, 4, 0, 0),
                Width = 100,
                Height = 90,
                Background = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri(address, UriKind.Relative))
                }
            };
            return grid;
        }
    }
}
