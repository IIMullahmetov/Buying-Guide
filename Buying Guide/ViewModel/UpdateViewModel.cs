using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Buying_Guide.Models;
using Buying_Guide.Models.DataBase;
using Buying_Guide.View;
using Buying_Guide.ViewModel;
using Xceed.Wpf.Toolkit;

namespace Buying_Guide.ViewModel
{

    class UpdateViewModel : ViewModelBase
    {
        private readonly Update _update;

        private string Shop
        {
            get { return _shop.SHOP1; }
            set
            {
                _shop.SHOP1 = value;
                OnPropertyChanged("ShopName");
            }
        }

        private string Address
        {
            get { return _shop.ADDRESS; }
            set
            {
                _shop.ADDRESS = value;
                OnPropertyChanged("Address");
            }
        }

        private string Phone
        {
            get { return _shop.PHONE; }
            set
            {
                _shop.PHONE = value;
                OnPropertyChanged("Phone");
            }
        }

        private string Image
        {
            get { return _shop.IMAGE; }
            set
            {
                _shop.IMAGE = value;
                OnPropertyChanged("ImageFile");
            }
        }

        private List<List<TimePicker>> WorkHours { get; set; }

        private readonly SHOP _shop;

        public UpdateViewModel(string id, Catalog catalog)
        {
            _update = new Update(Convert.ToInt32(id));
            _shop = _update.GetShop();
            InitializeFields();
        }

        private void InitializeFields()
        {
            Shop = _shop.SHOP1;
            Address = _shop.ADDRESS;
            Phone = _shop.PHONE;
            Image = _shop.IMAGE;
        }
    }
}
