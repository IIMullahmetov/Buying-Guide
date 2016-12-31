using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Buying_Guide.Models;
using Buying_Guide.View;

namespace Buying_Guide.ViewModel
{
    class Admin : ViewModelBase
    {
        private readonly ShopModel _model = new ShopModel();
        private readonly List<string> _addresses;
        private readonly List<string> _images;
        private readonly List<string> _shops;
        private readonly List<string> _phones;
        private readonly List<int> _shopsId;
        private readonly string[] _commands = { "insert", "drop", "delete", "update", "select", "create" };
        private readonly List<string> _specializations;
        private readonly List<string> _ownForms;
        private readonly AdminWindow _adminWindow;

        public Admin()
        {
            _adminWindow = new AdminWindow();
            _adminWindow.Show();
            _addresses = _model.GetAddresses();
            _shops = _model.GetShops();
            _shopsId = _model.GetShopsId();
            _specializations = _model.GetSpecialzations();
            _phones = _model.GetPhones();
            _images = _model.GetImages();
            _ownForms = _model.GetOwnForms();
            CreateWindow();
        }

        private void CreateWindow()
        {
            _adminWindow.StackPanel.Children.Clear();
            for (int j = 0; j < _shops.Count; j++)
                _adminWindow.StackPanel.Children.Add(GetGrid(j));
        }

        private Grid GetGrid(int i)
        {
            Grid grid = new Grid
            {
                Name = "n" + _shopsId[i],
                Height = 100,
                Width = 720,
                Background = Brushes.AliceBlue,
                Margin = new Thickness(20, 6, 6, 6)
            };
            grid.Children.Add(GetImageGrid(_images[i]));
            grid.Children.Add(GetAddressLabel(_addresses[i]));
            grid.Children.Add(GetPhoneLabel(" Телефон:  " + _phones[i]));
            grid.MouseUp += Go;
            return grid;
        }

        private void Go(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            Catalog catalog = new Catalog(grid.Name.Replace("n", ""))
            {
                Title = grid.Name.Replace("n", "")
            };
            catalog.Show();
        }

        private UIElement GetAddressLabel(string address)
        {
            Label label = new Label
            {
                Content = address,
                Margin = new Thickness(0, 0, 6, 25),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            return label;
        }

        private UIElement GetPhoneLabel(string address)
        {
            Label label = new Label
            {
                Content = address,
                Margin = new Thickness(0, 0, 6, 5),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            return label;
        }

        private Grid GetImageGrid(string address)
        {
            Grid grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(4, 4, 0, 0),
                Width = 250,
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
