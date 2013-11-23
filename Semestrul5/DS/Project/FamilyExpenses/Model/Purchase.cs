using System;

namespace FamilyExpenses.Model
{
    public class Purchase
    {
        public Purchase(int price, int quantity, DateTime purchaseDate, Person purchaser, Shop shop, Product product)
        {
            if (purchaser != null)
                if (shop != null)
                    if (product != null)
                    {
                        _price = price;
                        _quantity = quantity;
                        _purchaseDate = purchaseDate;
                        _purchaser = purchaser;
                        _shop = shop;
                        _product = product;
                    }
                    else
                        throw new ArgumentNullException("product");
                else
                    throw new ArgumentNullException("shop");
            else
                throw new ArgumentNullException("purchaser");
        }

        public int Price
        {
            get
            {
                return _price;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public DateTime PurchaseDate
        {
            get
            {
                return _purchaseDate;
            }
        }

        public Person Purchaser
        {
            get
            {
                return _purchaser;
            }
        }

        public Shop Shop
        {
            get
            {
                return _shop;
            }
        }

        public Product Product
        {
            get
            {
                return _product;
            }
        }

        private readonly int _price;
        private readonly int _quantity;
        private readonly DateTime _purchaseDate;
        private readonly Person _purchaser;
        private readonly Shop _shop;
        private readonly Product _product;
    }
}
