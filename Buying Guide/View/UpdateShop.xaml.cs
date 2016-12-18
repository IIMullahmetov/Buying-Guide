using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Buying_Guide.Models;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Buying_Guide.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для UpdateShop.xaml
    /// </summary>
    public partial class UpdateShop : Window
    {
        private readonly Update _update;
        private readonly List<string> _specializations;
        private readonly List<List<TimePicker>> list = new List<List<TimePicker>>();
        private readonly List<string> _weekDays;
        private readonly ShopModel _shopModel = new ShopModel();
        private readonly List<int> _ownFormListId;
        private readonly List<string> _ownForms;
        private readonly string _pattern;
        private readonly string[] _commands = { "insert", "drop", "delete", "update", "select", "create" };

        public UpdateShop(string i)
        {
            InitializeComponent();
            _pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            int id = Convert.ToInt32(i);    
            _update = new Update(id);
            _specializations = _shopModel.GetSpecialzations();
            _ownFormListId = _shopModel.GetOwnFormsId();
            _ownForms = _shopModel.GetOwnForms();
            _weekDays = _shopModel.GetWeekDays();  
            Initialize();          
        }

        private void Initialize()  
        {
            ShopName.Text = _update.GetName();
            Address.Text = _update.GetAddress();
            ImageFile.Text = _update.GetImage();
            Phone.Text = _update.GetPhone();
            double height = -15;
            var shopSpecializatons = _update.GetSpecializations();
            foreach (var s in _specializations)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Name = s.Replace(" ", "_").Replace(",", "1"),
                    Content = s,
                    Margin = new Thickness(5, height += 19, 5, 5)
                };
                if (shopSpecializatons.Contains(checkBox.Content.ToString()))
                    checkBox.IsChecked = true;
                Specializations.Children.Add(checkBox);
            }
            height = -15;
            int ownFormId = _update.GetOwnFormId();
            string form = _update.GetOwnWorm(ownFormId);
            foreach (var ownForm in _ownForms)
            {
                RadioButton radioButton = new RadioButton
                {
                    Name = ownForm.Replace(" ", "_").Replace(",", "0"),
                    Content = ownForm,
                    Margin = new Thickness(5, height += 19, 5, 5)
                };
                if (radioButton.Content.ToString() == form)
                    radioButton.IsChecked = true;
                OwnFroms.Children.Add(radioButton);
            }
            
            CreateTimePickers();
            var workHours = _update.GetWorkingHours();
            try
            {
                for (int i = 0; i < list[0].Count; i++)
                {
                    list[0][i].Text = workHours[0][i];
                    list[1][i].Text = workHours[1][i];
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                
            }
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            List<string> specializatonsList = new List<string>();
            foreach (CheckBox specialization in Specializations.Children)
                if (specialization.IsChecked != null && (bool)specialization.IsChecked)
                    specializatonsList.Add(specialization.Content.ToString());
            if (!CheckString(ShopName.Text))
            {
                MessageBox.Show(@"Недопустимой формат названия торговой точки!");
                return;
            }
            if (!CheckString(Address.Text))
            {
                MessageBox.Show(@"Недопустимой формат адреса торговой точки!");
                return;
            }
            if (!CheckImageString(ImageFile.Text))
            {
                MessageBox.Show(@"Недопустимой формат адреса изображения!");
                return;
            }
            if (!CheckString(Phone.Text))
            {
                MessageBox.Show(@"Недопустимой формат телефона!");
                return;
            }
            if (!Regex.IsMatch(Phone.Text, _pattern))
            {
                MessageBox.Show(@"Недопустимой формат телефона!");
                return;
            }
            if (specializatonsList.Count == 0)
            {
                MessageBox.Show(@"Укажите специализацию!");
                return;
            }
            if (GetOwnFormId() == 0)
            {
                MessageBox.Show(@"Укажите форму собственности");
                return;
            }
            if (
                !_update.UpdateShop(ShopName.Text, Address.Text, ImageFile.Text, Phone.Text, specializatonsList, GetOwnFormId(), list))
                MessageBox.Show(@"Ошибка при обновлении данных");
            Close();
            MessageBox.Show(@"Торговая точка успешно обновлена");
        }

        private int GetOwnFormId()
        {
            foreach (RadioButton radioButton in OwnFroms.Children)
                if (radioButton.IsChecked != null && (bool)radioButton.IsChecked)
                    return _ownFormListId[(_ownForms.IndexOf(radioButton.Name))];
            return 0;
        }

        private bool CheckImageString(string s)
        {
            if (_commands.Contains(s.ToLower()) || s.Length > 300)
                return false;
            return true;
        }

        private bool CheckString(string s)
        {
            if (s.Length > 50 || _commands.Contains(s.ToLower()))
                return false;
            if (s.Replace(" ", "") == "")
                return false;
            return true;
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateTimePickers()
        {
            int l = 20;
            int u = 30;
            int r = 658;
            int d = 43;
            string n = "O";
            string t = "08:00";
            for (int i = 0; i < 2; i++)
            {
                list.Add(new List<TimePicker>());
                foreach (string weekDay in _weekDays)
                {
                    l += 90;
                    r -= 90;
                    TimePicker timePicker = new TimePicker()
                    {
                        Margin = new Thickness(l, u, r, d),
                        Height = 25,
                        Width = 80,
                        Name = weekDay + n,
                        Text = t
                    };
                    WT.Children.Add(timePicker);
                    list[i].Add(timePicker);
                }
                u += 30;
                r = 658;
                l = 20;
                d -= 30;
                n = "C";
                t = "22:00";
            }
        }

        private void ImageFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (!_update.Remove())
                MessageBox.Show(@"Ошибка при удалении");
            else
                MessageBox.Show(@"Торговая точка успешно удалена");
        }
    }
}
