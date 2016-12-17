﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace Buying_Guide.Models
{
    class ShopModel
    {
        //private List<ArrayList> list = new List<ArrayList>();
        private readonly List<string> _addresses = new List<string>();
        private readonly List<string> _images = new List<string>();
        private readonly List<string> _shops = new List<string>();
        private readonly List<string> _specializations = new List<string>();
        private readonly List<int> _specializationsId = new List<int>();
        private readonly List<string> _phones = new List<string>();
        private readonly List<string> _ownForms = new List<string>();
        private readonly ORM _orm = new ORM();
        private readonly List<int> _ownFormsId = new List<int>();
        private readonly List<string> _daysOfWeek = new List<string>();
        private readonly List<int> _daysOfWeekId = new List<int>();

        public ShopModel()
        {
            var shopInfo =
                from shop in _orm.SHOP
                select new {shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};

            foreach (var shop in shopInfo)
            {
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }

            var specializations = from spec in _orm.SPECIALIZATION
                select new {spec.SPECIALIZATION1, spec.ID};

            foreach (var specialization in specializations)
            {
                _specializations.Add(specialization.SPECIALIZATION1);
                _specializationsId.Add(specialization.ID);
            }
            var ownForms = from of in _orm.OWN_FORMS
                select new { of.OWN_FORMS1, of.ID};

            foreach (var ownForm in ownForms)
            {
                _ownForms.Add(ownForm.OWN_FORMS1);
                _ownFormsId.Add(ownForm.ID);
            }

            var dow = from d in _orm.WEEK_DAYS
                select new {d.DAY_OF_WEEK, d.ID};
            foreach (var day in dow)
            {
                _daysOfWeek.Add(day.DAY_OF_WEEK);
                _daysOfWeekId.Add(day.ID);
            }
        }

        public List<string> GetWeekDays()
        {
            return _daysOfWeek;
        }

        public List<int> GetOwnFormsId()
        {
            return _ownFormsId;
        }
        
        public List<string> GetOwnForms()
        {
            return _ownForms;
        }

        public List<string> GetAddresses()
        {
            return _addresses;
        }

        public List<string> GetShops()
        {
            return _shops;
        }

        public List<string> GetImages()
        {
            return _images;
        }

        public List<string> GetSpecialzations()
        {
            return _specializations;
        }

        public List<string> GetPhone()
        {
            return _phones;
        }

        public void SearchClick(string parametr)
        {
            var shopInfo =
                from shop in _orm.SHOP
                where shop.SHOP1.Contains(parametr)
                select new {shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};
            ClearList();
            foreach (var shop in shopInfo)
            {
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void AddShop(string name, string address, string phone, int ownForm, string image, List<string> specializationsList, List<List<TimePicker>> list) 
        {
            SHOP shop = new SHOP
            {
                SHOP1 = name,
                ADDRESS = address,
                PHONE = phone,
                OWN_FORM_ID = ownForm,
                IMAGE = @"..\..\Images\not found.jpg"
            };

            if (File.Exists(image))
                shop.IMAGE = image;
            
            _orm.SHOP.Add(shop);
           
            List<int> specList = GetSpecializationId(specializationsList);
            foreach (int i in specList)
                _orm.SHOP_SPECIALIZATION.Add(new SHOP_SPECIALIZATION() {SHOP_ID = shop.ID, SPECIALIZATION_ID = i});
            for (int i = 0; i < list.Count; i++)
            {
                shop.WORKING_HOURS.Add(new WORKING_HOURS()
                {
                    OPEN_TIME = list[0][i].Text,
                    CLOSE_TIME = list[1][i].Text,
                    DAY_OF_WEEK_ID = _daysOfWeekId[i],
                    SHOP_ID = shop.ID
                });
            }
            _orm.SaveChanges();
        }


        private List<int> GetSpecializationId(List<string> specializationsList)
        {
            List<int> list = new List<int>();
            foreach (string s in specializationsList)
                list.Add(_specializationsId[_specializations.IndexOf(s)]);

            return list;
        }
        
        private void ClearList()
        {
            _shops.Clear();
            _addresses.Clear(); 
            _images.Clear();
            _phones.Clear();
        }
        public void Find(string ownForm)
        {
            var shopInfo = from shop in _orm.SHOP
                join form in _orm.OWN_FORMS on shop.OWN_FORM_ID equals form.ID
                where form.OWN_FORMS1 == ownForm
                select new {shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};

            ClearList();

            foreach (var shop in shopInfo)
            {
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void Find(List<string> list, string ownForm)
        {
            ClearList();
            var shopInfo = from shop in _orm.SHOP
                join own_form in _orm.OWN_FORMS on shop.OWN_FORM_ID equals own_form.ID
                join specialization in _orm.SPECIALIZATION on shop.ID equals specialization.ID
                where own_form.OWN_FORMS1.Equals(ownForm) && list.Contains(specialization.SPECIALIZATION1)
                select new {shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};

            foreach (var shop in shopInfo)
            {
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void Find(List<string> specializationList)
        {
            ClearList();

            var shopInfo = from shop in _orm.SHOP
                join spec in _orm.SPECIALIZATION on shop.ID equals spec.ID
                where specializationList.Contains(spec.SPECIALIZATION1)
                select new { shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE };

            foreach (var shop in shopInfo)
            {
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }
    }
}