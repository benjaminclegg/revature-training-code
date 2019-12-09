using System;
using PizzaBox.Domain.Factories;
using PizzaBox.Client.Singletons;
using PizzaBox.Client.MockedData;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           var program = new Program();
           program.ApplicationStart();
        }
        
        private static UserRepository _ur = new UserRepository();
        private static StoreRepository _sr = new StoreRepository();
        private static OrderRepository _or = new OrderRepository();
        private static PizzaRepository _pr = new PizzaRepository();

        private void ApplicationStart()
        {
           var uf = new UserFactory();
           var sf = new StoreFactory();
           var of = new OrderFactory();
           var pf = new PizzaFactory();

           var fakeData = new FakeData();
           _ur = fakeData.InitializeUsers(uf, _ur);
           _sr = fakeData.InitializeStores(sf, _sr);

           var applicationMenu = MenuSingleton.Instance;
           applicationMenu.Execute(uf, sf, of, pf, _ur, _sr, _or, _pr);
        }
    }
}