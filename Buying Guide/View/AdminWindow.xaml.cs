using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private List<int> _shopsId;
        private readonly string[] _commands = { "insert", "drop", "delete", "update", "select", "create" };

        //Данный класс отвечает за отображение элементов в стартовом окне, графику объяснять сложнааа, поэтому лучше перейти в классы в папке Models, сперва ShopModel
        public AdminWindow()// Инициализируем объекты
        {
            InitializeComponent();
            
        } 
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckString(Query.Text))
            {
                MessageBox.Show("Попытка взлома, приложение будет закрыто!");
                Environment.Exit(0);
            }
            _model.SearchClick(Query.Text);
            //CreateWindow();
        }

        private bool CheckString(string s)
        {
            if (s.Length > 50 || _commands.Contains(s.ToLower()))
                return false;
            return true;
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
                //CreateWindow();
                return;
            }
            if (specList.Count > 0)
                _model.Find(specList);
            if(ownForm != null)
                _model.Find(ownForm);
            //CreateWindow();
        }
        
        private void SearchString(object sender, TextChangedEventArgs e)
        {

        }
        
        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            AddShop addShop = new AddShop();
            addShop.Show();
        }

        public void UpdateGridClick(object sender, RoutedEventArgs e)
        {
            _model.UpdateWindow();
            //CreateWindow();
        }
    }
}