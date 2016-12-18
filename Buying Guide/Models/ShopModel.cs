using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using Buying_Guide.Models.DataBase;
using Xceed.Wpf.Toolkit;

namespace Buying_Guide.Models
{
    class ShopModel
    {
        //Создаем всяческие листы, необходимые для отображения и редактирования
        private readonly List<string> _addresses = new List<string>();
        private readonly List<string> _images = new List<string>();
        private readonly List<string> _shops = new List<string>();
        private readonly List<int> _shopsId = new List<int>();
        private readonly List<string> _specializations = new List<string>();
        private readonly List<int> _specializationsId = new List<int>();
        private readonly List<string> _phones = new List<string>();
        private readonly List<string> _ownForms = new List<string>();
        private readonly ORM _orm = new ORM();//создаем контекст для работы с базой данных 
        private readonly List<int> _ownFormsId = new List<int>();
        private readonly List<string> _daysOfWeek = new List<string>();
        private readonly List<int> _daysOfWeekId = new List<int>();
        private SHOP shop; //Не инициализированный объект сущности, дальше будет нужен для добавления данных в БД

        public ShopModel()
        {
            var shopInfo = //получаем поля из таблицы Шоп и вроде бы все
                from shop in _orm.SHOP
                select new {shop.ID, shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};

            foreach (var shop in shopInfo) //добавляем эти поля в соответсвующие листы, они периодически будут нам нужны
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }

            var specializations = from spec in _orm.SPECIALIZATION //получаем специализации и 48-52 строках добавляем их в листы
                select new {spec.SPECIALIZATION1, spec.ID};

            foreach (var specialization in specializations)
            {
                _specializations.Add(specialization.SPECIALIZATION1);
                _specializationsId.Add(specialization.ID);
            }
            var ownForms = from of in _orm.OWN_FORMS//получем формы собственности и так их добавляем в листы
                           select new { of.OWN_FORMS1, of.ID};

            foreach (var ownForm in ownForms)
            {
                _ownForms.Add(ownForm.OWN_FORMS1);
                _ownFormsId.Add(ownForm.ID);
            }

            var dow = from d in _orm.WEEK_DAYS//опять получаем дни недели и суем их в листы
                      select new {d.DAY_OF_WEEK, d.ID};
            foreach (var day in dow)
            {
                _daysOfWeek.Add(day.DAY_OF_WEEK);
                _daysOfWeekId.Add(day.ID);
            }
        }

        public List<int> GetShopsId()// возвращает лист с АЙДИшками магазина
        {
            return _shopsId;
        }

        public List<string> GetWeekDays()//возвращает дни недели и так еще все методы с 76 по 115 возвращают соответсвующие таблицы
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

        public void SearchClick(string parametr)//вызывается при нажатии кнопки найти в главном окне, получает параметр и ищет наличие данной подстроки в названии магазина
        {
            var shopInfo =
                from shop in _orm.SHOP
                where shop.SHOP1.Contains(parametr)
                select new {shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE, shop.ID};
            ClearList();
            foreach (var shop in shopInfo)
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public bool AddShop(string name, string address, string phone, int ownForm, string image)//это вызывается при добавлении магазина, он добавляет данные в таблицу SHOP 
        {
            shop = new SHOP//присваиваем созданному заранее экземпляру магазина новую ссылку и присваиваем его полям значение
            {
                SHOP1 = name,
                ADDRESS = address,
                PHONE = phone,
                OWN_FORM_ID = ownForm,
                IMAGE = @"..\..\Images\not found.jpg"//значение по умолчанию задано, но я все решил написать какой файл изображения будет если ничего не добавлять
            };

            if (File.Exists(image))
                shop.IMAGE = image;//путь менятся только если изображение существует

            _orm.SHOP.Add(shop);//добавляем данный экземпляр в базу данных, фактический готовится запрос на добавление, но еще не выполняется
            try
            {
                _orm.SaveChanges(); //вот щас выполняется наш запрос в случае ошибки вернется boolean значение false и выводтся соответсвующая ошибка пользователю
            }
            catch (Exception e)
            {
                return false;
            }
            return true;//иначе начинается добавление данных в другие таблицы
        }

        public bool AddSpecializations(List<string> specializationsList)//например сюда
        {
            List<int> specList = GetSpecializationId(specializationsList);
            foreach (int i in specList)
                _orm.insertSpec(shop.ID, i);//токо здесь по другому, вызывается процедура добавления, котрый описан в методы insertSpec в классе ORM
            try
            {
                _orm.SaveChanges();//снова сохраняем
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool AddWorkHours(List<List<TimePicker>> list)//добавляем времена работы, а время работы мы берем из экземпляров графики попытка ввести фигню, все равно отработает правильно и без лагов
        {
            for (int i = 0; i < list[0].Count; i++)
                _orm.insertWorkHours(_daysOfWeekId[i], shop.ID, list[0][i].Text, list[1][i].Text);
            return true;
        }


        private List<int> GetSpecializationId(List<string> specializationsList)//этот метод возвращет АйДи полей по которым специализируется магазин
        {
            List<int> list = new List<int>();
            foreach (string s in specializationsList)
                list.Add(_specializationsId[_specializations.IndexOf(s)]);
            return list;
        }
        
        private void ClearList()//здесь очищаем листы, это надо для поиска магазина
        {
            _shopsId.Clear();
            _shops.Clear();
            _addresses.Clear(); 
            _images.Clear();
            _phones.Clear();
        }
        public void Find(string ownForm)//метод выполняет подбор, если указана только форма собственности
        {
            var shopInfo = from shop in _orm.SHOP
                join form in _orm.OWN_FORMS on shop.OWN_FORM_ID equals form.ID
                where form.OWN_FORMS1 == ownForm
                select new {shop.ID, shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE};

            ClearList();

            foreach (var shop in shopInfo)
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void Find(List<string> list, string ownForm)//метод выполняет подбор если указаны и специализации и форма собственности 
        {
            var spec = GetSpecializationId(list);
            ClearList();
            var shopInfo = (from shop in _orm.SHOP
                join own_form in _orm.OWN_FORMS on shop.OWN_FORM_ID equals own_form.ID
                join specialization in _orm.SHOP_SPECIALIZATION on shop.ID equals specialization.SHOP_ID
                where own_form.OWN_FORMS1 == ownForm && spec.Contains(specialization.SPECIALIZATION_ID)
                select new {shop.ID, shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE}).Distinct();

            foreach (var shop in shopInfo)
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void Find(List<string> specializationList)//а этот подбирает если указана только форма собственности 
        {
            ClearList();

            var shopInfo = from shop in _orm.SHOP
                join spec in _orm.SPECIALIZATION on shop.ID equals spec.ID
                where specializationList.Contains(spec.SPECIALIZATION1)
                select new {shop.ID, shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE };

            foreach (var shop in shopInfo)
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void UpdateWindow() //вызывается когда нажата кнопка обновить на главной странице, очищает листы, заново получает данные из бд а окошко их показывает
        {
            var shopInfo =
                from shop in _orm.SHOP
                select new { shop.SHOP1, shop.IMAGE, shop.ADDRESS, shop.PHONE, shop.ID };
            ClearList();
            foreach (var shop in shopInfo)
            {
                _shopsId.Add(shop.ID);
                _shops.Add(shop.SHOP1);
                _addresses.Add(shop.ADDRESS);
                _images.Add(shop.IMAGE);
                _phones.Add(shop.PHONE);
            }
        }

        public void Generate()//метод на всякии случай, был нужен для генерации времени работы, когда все полетело к чертям
        {
            var list = GetShopsId();
            foreach (int i in list)
            {
                foreach (int i1 in _daysOfWeekId)
                {
                    _orm.insertWorkHours(i1, i, "08:00", "22:00");
                }
            }
        }
    }
}