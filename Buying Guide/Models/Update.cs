using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Xceed.Wpf.Toolkit;

namespace Buying_Guide.Models
{
    class Update
    {
        private readonly ORM _orm = new ORM();
        private readonly int _id;
        private readonly List<string> _specializations = new List<string>();
        private readonly List<int> _specializationsId = new List<int>();
        private readonly List<int> _weekDaysId = new List<int>();

        public Update(int id)
        {
            _id = id;
            var shopInfo = from shop in _orm.SPECIALIZATION
                select new {shop.ID, shop.SPECIALIZATION1};
            foreach (var shop in shopInfo)
            {
                _specializationsId.Add(shop.ID);
                _specializations.Add(shop.SPECIALIZATION1);
            }

            var weekDays = from week in _orm.WEEK_DAYS
                select new {week.ID};
            foreach (var day in weekDays)
                _weekDaysId.Add(day.ID);
        }

        public string GetPhone()
        {
            var shop = from s in _orm.SHOP
                where s.ID == _id
                select new {s.PHONE};
            string phone = null;
            foreach (var s in shop)
            {
                phone = s.PHONE;
            }
            return phone;
        }

        public string GetName()
        {
            var n = from shop in _orm.SHOP
                where shop.ID == _id
                select new {shop.SHOP1};
            string name = null;
            foreach (var s in n)
                name = s.SHOP1;
            return name;
        }

        public string GetOwnWorm(int id)
        {
            var shopInfo = from ownForm in _orm.OWN_FORMS
                where ownForm.ID == id
                select new {ownForm.OWN_FORMS1};
            string form = null;
            foreach (var s in shopInfo)
            {
                form = s.OWN_FORMS1;
            }
            return form;
        }

        public List<List<string>> GetWorkingHours()
        {
            var shopInfo = from workHours in _orm.WORKING_HOURS
                where workHours.SHOP_ID == _id
                select new {workHours.OPEN_TIME, workHours.CLOSE_TIME};
            List<List<string>> list = new List<List<string>>();
            list.Add(new List<string>());
            list.Add(new List<string>());
            foreach (var shop in shopInfo)
            {
                list[0].Add(shop.OPEN_TIME);
                list[1].Add(shop.CLOSE_TIME);
            }
            return list;
        }

        public List<string> GetSpecializations()
        {
            var specializations = from shop_spec in _orm.SHOP_SPECIALIZATION
                where shop_spec.SHOP_ID == _id
                join specialization in _orm.SPECIALIZATION on shop_spec.SPECIALIZATION_ID equals specialization.ID
                select new {specialization.SPECIALIZATION1};
            List<string> list = new List<string>();
            foreach (var specialization in specializations)
            {
                list.Add(specialization.SPECIALIZATION1);
            }
            return list;
        }

        private List<int> GetSpecializationId(List<string> specializationsList)
        {
            List<int> list = new List<int>();
            foreach (string s in specializationsList)
                list.Add(_specializationsId[_specializations.IndexOf(s)]);

            return list;
        }

        public bool UpdateShop(string name, string address, string image, string phone, List<string> specializations, int ownForm, List<List<TimePicker>> list)
        {
            var shop = _orm.SHOP.First(s => s.ID == _id);
            shop.SHOP1 = name;
            shop.ADDRESS = address;
            shop.IMAGE = image;
            shop.PHONE = phone;
            shop.OWN_FORM_ID = ownForm;

            foreach (var i in _weekDaysId)
            {
                WORKING_HOURS workingHours = _orm.WORKING_HOURS.First(w => w.SHOP_ID == _id);
                _orm.WORKING_HOURS.Attach(workingHours);
                _orm.WORKING_HOURS.Remove(workingHours);
                _orm.SaveChanges();
            }

            for (int i = 0; i < list[0].Count; i++)
                _orm.WORKING_HOURS.Add(new WORKING_HOURS()
                {
                    OPEN_TIME = list[0][i].Text,
                    CLOSE_TIME = list[1][i].Text,
                    SHOP_ID = _id,
                    DAY_OF_WEEK_ID = _weekDaysId[i]
                });
            _orm.SaveChanges();
            var q = GetSpecializations();
            foreach (var sss in q)
            {
                SHOP_SPECIALIZATION shopSpecialization = _orm.SHOP_SPECIALIZATION.First(s => s.SHOP_ID == _id);
                _orm.SHOP_SPECIALIZATION.Attach(shopSpecialization);
                _orm.SHOP_SPECIALIZATION.Remove(shopSpecialization);
                _orm.SaveChanges();
            }
            var specID = GetSpecializationId(specializations);
            foreach (int i in specID)
                _orm.SHOP_SPECIALIZATION.Add(new SHOP_SPECIALIZATION()
                {
                    SHOP_ID = _id,
                    SPECIALIZATION_ID = i
                });
            try
            {
                _orm.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public string GetAddress()
        {
            var n = from shop in _orm.SHOP
                    where shop.ID == _id
                    select new { shop.ADDRESS };
            string address = null;
            foreach (var s in n)
                address = s.ADDRESS;
            return address;
        }

        public string GetImage()
        {
            var n = from shop in _orm.SHOP
                    where shop.ID == _id
                    select new { shop.IMAGE };
            string image = null;
            foreach (var s in n)
                image = s.IMAGE;
            return image;
        }

        public int GetOwnFormId()
        {
            var n = from shop in _orm.SHOP
                    where shop.ID == _id
                    select new { shop.OWN_FORM_ID };
            int ownForm = 0;
            foreach (var s in n)
                ownForm = s.OWN_FORM_ID;
            return ownForm;
        }

        public bool Remove()
        {
            try
            {
                using (_orm)
                {
                    var specializations = GetSpecializations();
                    foreach (var specialization in specializations)
                    {
                        SHOP_SPECIALIZATION shopSpecialization = _orm.SHOP_SPECIALIZATION.First(s => s.SHOP_ID == _id);
                        _orm.SHOP_SPECIALIZATION.Attach(shopSpecialization);
                        _orm.SHOP_SPECIALIZATION.Remove(shopSpecialization);
                        _orm.SaveChanges();
                    }
                    foreach (var i in _weekDaysId)
                    {
                        WORKING_HOURS workingHours = _orm.WORKING_HOURS.First(w => w.SHOP_ID == _id);
                        _orm.WORKING_HOURS.Attach(workingHours);
                        _orm.WORKING_HOURS.Remove(workingHours);
                        _orm.SaveChanges();
                    }

                    SHOP shop = _orm.SHOP.First(s => s.ID == _id);
                    _orm.SHOP.Attach(shop);
                    _orm.SHOP.Remove(shop);
                    _orm.SaveChanges();
                }
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
            catch (AggregateException e)
            {
                return false;
            }
            return true;
        }
    }
}
