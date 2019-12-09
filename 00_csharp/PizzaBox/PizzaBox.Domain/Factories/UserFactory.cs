using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Factories
{
   public class UserFactory : IUserFactory
   {
      public User Create()
      {
         return new User();
      }
   }
}