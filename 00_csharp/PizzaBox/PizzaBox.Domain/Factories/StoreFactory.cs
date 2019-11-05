using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Factories
{
   public class StoreFactory : IStoreFactory
   {
      public Store Create()
      {
         return new Store();
      }
   }
}