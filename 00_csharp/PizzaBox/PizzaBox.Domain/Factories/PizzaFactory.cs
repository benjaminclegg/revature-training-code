using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Factories
{
   public class PizzaFactory : IPizzaFactory
   {
      public Pizza Create()
      {
         return new Pizza();
      }
   }
}