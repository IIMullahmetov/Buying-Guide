using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Buying_Guide.Models;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace Buying_Guide.View
{
    /// <summary>
    /// Логика взаимодействия для AddShop.xaml
    /// </summary>
    public partial class AddShop : Window
    {
        private readonly ShopModel _model = new ShopModel();
        private readonly List<int> _ownFormListId;
        private readonly string _pattern;
        private readonly List<string> _weekDays;
        private List<List<TimePicker>> list = new List<List<TimePicker>>();


        private readonly List<string> _ownForms;
        public AddShop()
        {
            _pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            InitializeComponent();
            _ownForms = _model.GetOwnForms();
            _ownFormListId = _model.GetOwnFormsId();
            _weekDays = _model.GetWeekDays();
            var specializations = _model.GetSpecialzations();
            double height = -15;
            foreach (var s in specializations)
                Specializations.Children.Add(new CheckBox()
                {
                    Name = s.Replace(" ", "_").Replace(",", "1"),
                    Content = s,
                    Margin = new Thickness(5, height+=19, 5, 5)
                });
            
            height = -15;
            foreach (var ownForm in _ownForms)
                OwnFroms.Children.Add(new RadioButton
                {
                    Name = ownForm.Replace(" ", "_").Replace(",", "0"),
                    Content = ownForm,
                    Margin = new Thickness(5, height += 19, 5, 5)
                });
            
            CreateTimePickers();
            //TimeSpan time = new TimeSpan(23, 59, 0);
            //string t = list[1][6].Text;
            //time = TimeSpan.Parse(t);
            //MessageBox.Show(time.ToString());
            //MessageBox.Show(SundayO.Text);

        }

        private int GetOwnFormId()
        {
            foreach (RadioButton radioButton in OwnFroms.Children)
                if (radioButton.IsChecked != null && (bool) radioButton.IsChecked)
                    return _ownFormListId[(_ownForms.IndexOf(radioButton.Name))];
            return 0;
        }
        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<string> specializatonsList = new List<string>();
            foreach (CheckBox specialization in Specializations.Children)
                if (specialization.IsChecked != null && (bool)specialization.IsChecked)
                    specializatonsList.Add(specialization.Content.ToString());
            
            if (!CheckString(Name.Text))
            {
                MessageBox.Show("Недопустимой формат названия торговой точки!");
                return;
            }
            if (!CheckString(Address.Text))
            {
                MessageBox.Show("Недопустимой формат адреса торговой точки!");
                return;
            }
            if (!CheckImageString(Image.Text))
            {
                MessageBox.Show("Недопустимой формат адреса изображения!");
                return;
            }
            if (!CheckString(Phone.Text))
            {
                MessageBox.Show("Недопустимой формат телефона!");
                return;
            }
            if (!Regex.IsMatch(Phone.Text, _pattern))
            {
                MessageBox.Show("Недопустимой формат телефона!");
                return;
            }


            if (specializatonsList.Count == 0)
            {
                MessageBox.Show("Специализация не выбрана!");
                return;
            }
            if (GetOwnFormId() == 0)
            {
                MessageBox.Show("Укажите форму собственности");
                return;
            }
            
            _model.AddShop(Name.Text, Address.Text, Phone.Text, GetOwnFormId(), Image.Text, specializatonsList, list);
        }

        private bool CheckImageString(string s)
        {
            if (s.ToLower().Contains("drop") || s.ToLower().Contains("insert") || s.ToLower().Contains("select") || s.ToLower().Contains("update"))
                return false;
            return true;
        }
        private bool CheckString(string s)
        {
            if (s.ToLower().Contains("drop") || s.ToLower().Contains("insert") || s.ToLower().Contains("select") || s.ToLower().Contains("update"))
                return false;
            if (s.Replace(" ", "") == "")
                return false;
            return true;
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

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            openFile.ShowDialog();
            Image.Text = openFile.FileName;
        }
    }
}
