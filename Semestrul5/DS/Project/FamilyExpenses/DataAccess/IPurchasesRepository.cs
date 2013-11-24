using System;
using System.Collections.Generic;
using FamilyExpenses.Model;

namespace FamilyExpenses.DataAccess
{
    public interface IPurchasesRepository
    {
        int TotalCost(Person person);

        void Import();

        void Export();

        IReadOnlyCollection<Product> GetProducts();

        IReadOnlyCollection<Producer> GetProducers();

        IReadOnlyCollection<Shop> GetShops();

        IReadOnlyCollection<Address> GetAddresses();

        IReadOnlyCollection<Purchase> GetPurchasesBetweenDates(Person person, DateTime startDate, DateTime endDate);

        IReadOnlyCollection<Purchase> GetPurchasesFrom(Person person, Producer producer);

        IReadOnlyCollection<Purchase> GetPurchasesFrom(Person person, Shop shop);

        IReadOnlyCollection<Purchase> GetPurchases(Person person, DateTime startDate, DateTime endDate);

        void Add(Purchase purchase);

        void Update(Purchase oldPurchase, Purchase newPurchase);

        void Delete(Purchase purchase);
    }
}
