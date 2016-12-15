using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buying_Guide.Models
{

    class CatalogModel
    {
        private readonly ORM _orm = new ORM();
        private List<PRODUCTS> _products = new List<PRODUCTS>();
        

        public List<PRODUCTS> GetProducts()
        {
            return _products;
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
    }
}
