using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        private readonly ShopModel _model = new ShopModel();
        private readonly AdminWindow _adminWindow;
        public Catalog(AdminWindow adminWindow)
        {
            _adminWindow = adminWindow;
            InitializeComponent();

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Add_Product_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Search_String(object sender, TextChangedEventArgs e)
        {

        }
        
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateShop update = new UpdateShop(Title);
            update.Show();
        }
    }
}
