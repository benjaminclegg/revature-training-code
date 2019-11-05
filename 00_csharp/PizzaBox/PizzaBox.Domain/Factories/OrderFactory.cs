using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Factories
{
   public class OrderFactory : IOrderFactory
   {
      public Order Create()
      {
         return new Order();
      }
   }
}