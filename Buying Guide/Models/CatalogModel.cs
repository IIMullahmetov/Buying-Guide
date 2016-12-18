using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buying_Guide.Models.DataBase;

namespace Buying_Guide.Models
{

    class CatalogModel
    {
        private readonly ORM _orm = new ORM();
        private List<PRODUCTS> _products = new List<PRODUCTS>();
        private int _id;

        public CatalogModel(string i)
        {
            int id = Convert.ToInt32(i);
            _id = id;
        }
        public void Find(string name)
        {
            var product = from products in _orm.PRODUCTS
                where products.PRODUCT.Equals(name)
                select new {};
            foreach (var prod in product)
            {
                
            }
        }

        public List<Decimal> GetPrice()
        {
            List<Decimal> list = new List<Decimal>();
            var products = from p in _orm.PRODUCTS
                           where p.SHOP_ID == _id
                           select new { p.PRICE };
            foreach (var product in products)
            {
                list.Add(product.PRICE);
            }
            return list;
        }
        public List<int> GetCount()
        {
            List<int> list = new List<int>();
            var products = from p in _orm.PRODUCTS
                           where p.SHOP_ID == _id
                           select new { p.COUNT };
            foreach (var product in products)
            {
                list.Add(product.COUNT);
            }
            return list;
        }
        public List<string> GetDiscription()
        {
            List<string> list = new List<string>();
            var products = from p in _orm.PRODUCTS
                           where p.SHOP_ID == _id
                           select new { p.DESCRIPTION };
            foreach (var product in products)
            {
                list.Add(product.DESCRIPTION);
            }
            return list;
        }

        public List<string> GetProducts()
        {
            List<string> list = new List<string>();
            var products = from p in _orm.PRODUCTS
                           where p.SHOP_ID == _id
                select new { p.PRODUCT };
            foreach (var product in products)
            {
                list.Add(product.PRODUCT);
            }
            return list;
        }

        public List<string> GetImages()
        {
            List<string> list = new List<string>();
            var products = from p in _orm.PRODUCTS
                           where p.SHOP_ID == _id
                           select new {p.IMAGE};
            foreach (var product in products)
            {
                list.Add(product.IMAGE);
            }
            return list;
        }
    }
}
