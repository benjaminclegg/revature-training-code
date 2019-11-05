using System.Collections.Generic;
using PizzaBox.Domain.Factories;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.MockedData
{
   public class FakeData
   {
        public UserRepository InitializeUsers(UserFactory uf, UserRepository _ur)
        {
           var manager1 = uf.Create<Manager>();
           manager1.UserType = "Manager";
           manager1.FirstName = "Bob";
           manager1.LastName = "Bobson";
           manager1.Phone = "555-000-0000";
           manager1.Address = "100 Home St";
           manager1.Email = "bobbobson@email.com";
           manager1.StoreID = 1;
           _ur.Persist(manager1);

           var manager2 = uf.Create<Manager>();
           manager2.UserType = "Manager";
           manager2.FirstName = "Joe";
           manager2.LastName = "Joeson";
           manager2.Phone = "555-000-0001";
           manager2.Address = "102 Home St";
           manager2.Email = "joejoeson@email.com";
           manager2.StoreID = 2;
           _ur.Persist(manager2);

           var manager3 = uf.Create<Manager>();
           manager3.UserType = "Manager";
           manager3.FirstName = "Ben";
           manager3.LastName = "Benson";
           manager3.Phone = "555-000-0002";
           manager3.Address = "104 Home St";
           manager3.Email = "benbenson@email.com";
           manager3.StoreID = 3;
           _ur.Persist(manager3);

           var user1 = uf.Create<Customer>();
           user1.UserType = "Customer";
           user1.FirstName = "Benjamin";
           user1.LastName = "Clegg";
           user1.Phone = "555-555-5555";
           user1.Address = "999 Home St";
           user1.Email = "b.clegg@live.com";
           user1.StoreID = 2;
           _ur.Persist(user1);

           return _ur;
        }

        public StoreRepository InitializeStores(StoreFactory sf, StoreRepository _sr)
        {
           var store1 = sf.Create();
           store1.StoreID = 1;
           store1.Address = "123 main st";
           _sr.Persist(store1);

           var store2 = sf.Create();
           store2.StoreID = 2;
           store2.Address = "456 main st";
           _sr.Persist(store2);

           var store3 = sf.Create();
           store3.StoreID = 3;
           store3.Address = "789 main st";
           _sr.Persist(store3);

           return _sr;
        }
   }
}