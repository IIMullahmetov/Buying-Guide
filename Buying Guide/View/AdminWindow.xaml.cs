﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Buying_Guide.Models;

namespace Buying_Guide.View
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly ShopModel _model = new ShopModel();

        private List<string> _addresses;
        private List<string> _images;
        private List<string> _shops;
        private List<string> _phone;
        private ArrayList _list;
        

        public AdminWindow()
        {
            InitializeComponent();
            List<string> specializations = _model.GetSpecialzations();
            List<string> ownForms = _model.GetOwnForms();
            double height = -15;
            foreach (var specialize in specializations)
            {
                spec.Children.Add(new CheckBox
                {
                    Name = specialize.Replace(" ", "_").Replace(",", "0"),
                    Content = specialize,
                    Margin = new Thickness(5, height+=18, 5, 5)
                });
            }
            height = -15;
            foreach (var ownForm in ownForms)
            {
                own_form.Children.Add(new RadioButton
                {
                    Name = ownForm.Replace(" ", "_").Replace(",", "0"),
                    Content = ownForm,
                    Margin = new Thickness(5, height+=18, 5, 5)
                });
            }
            CreateWindow();

        }

        private void CreateWindow()
        {
            Background = Brushes.Gainsboro;
            _shops = _model.GetShops();
            _addresses = _model.GetAddresses();
            _images = _model.GetImages();
            _phone = _model.GetPhone();
            StackPanel.Children.Clear();
            for (int j = 0; j < _shops.Count; j++)
                StackPanel.Children.Add(GetGrid(j));
            InitializeComponent();
        }

        private Grid GetGrid(int i)
        {
            GC.Collect();
            Grid grid = new Grid
            {
                Name = _shops[i],
                Height = 100,
                Width = 720,
                Background = Brushes.White,
                Margin = new Thickness(20, 6, 6, 6)
            };
            grid.Children.Add(GetImageGrid(_images[i]));
            grid.Children.Add(GetAddressLabel(_addresses[i]));
            grid.Children.Add(GetPhoneLabel(" Телефон:  " + _phone[i]));
            grid.Children.Add(GetButton(_shops[i]));
            return grid;
        }

        private UIElement GetButton(string name)
        {
            Button button = new Button()
            {
                Content = "Перейти",
                Name = name,
                Width = 75,
                Height = 25,
                Margin = new Thickness(0, 0, 6, 4),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = Brushes.Gray,
                BorderBrush = Brushes.White
            };
            button.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Go));
            return button;
        }
        
        private UIElement GetAddressLabel(string address)
        {
            Label label = new Label
            {
                Content = address,
                Margin = new Thickness(0, 0, 6, 48),
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
                Margin = new Thickness(0, 0, 6, 28),
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
       
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _model.SearchClick(Query.Text);
            CreateWindow();
        }

        private void Go(object sender, RoutedEventArgs e)
        {
            Hide();
            Button button = (Button) sender;
            View.Catalog catalog = new View.Catalog(this)
            {
                Title = button.Name
            };
            catalog.Show();
        }

        private void Finder(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once PossibleInvalidOperationException

            List<string> specList = new List<string>();
            specList.Clear();
            foreach (CheckBox s in spec.Children)
                // ReSharper disable once PossibleInvalidOperationException
                if (s.IsChecked.Value)
                    specList.Add(s.Content.ToString());
            
            string ownForm = null;
            foreach (RadioButton child in own_form.Children)
                // ReSharper disable once PossibleInvalidOperationException
                if (child.IsChecked.Value)
                    ownForm = child.Name;
            if (specList.Count > 0 && ownForm != null)
            {
                _model.Find(specList, ownForm);
                CreateWindow();
                return;
            }
            if (specList.Count > 0)
                _model.Find(specList);
            if(ownForm != null)
                _model.Find(ownForm);
            CreateWindow();
        }
        
        private void SearchString(object sender, TextChangedEventArgs e)
        {
            
        }
        
        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            AddShop addShop = new AddShop();
            addShop.Show();
        }
    }
}